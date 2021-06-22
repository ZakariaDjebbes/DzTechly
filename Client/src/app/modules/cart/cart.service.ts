import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { Cart, ICart, ICartItem, ICartTotals } from 'src/app/shared/models/Cart';
import { IDeliveryMethod } from 'src/app/shared/models/IDeliveryMethod';
import { IProduct } from 'src/app/shared/models/IProduct';
import { environment } from 'src/environments/environment';
import { ShopService } from '../shop/shop.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  baseUrl = environment.apiUrl;
  shipping = 0.0;
  private cartSource = new BehaviorSubject<ICart>(null);
  private cartTotalSource = new BehaviorSubject<ICartTotals>(null);

  public cart$ = this.cartSource.asObservable();
  public cartTotal$ = this.cartTotalSource.asObservable();

  constructor(private http: HttpClient, private shopService: ShopService, private toastr: ToastrService) { }

  public setShippingPrice(deliveryMethod: IDeliveryMethod): void {
    this.shipping = deliveryMethod.price;
    const cart = this.getCurrentCartValue();
    cart.deliveryMethodId = deliveryMethod.id;
    this.calculateTotals();
    this.setCart(cart);
  }

  public createPaymentIntent() {
    return this.http.post(this.baseUrl + 'payment/' + this.getCurrentCartValue().id, {}).pipe(
      map((cart: ICart) => {
        this.cartSource.next(cart);
      })
    );
  }

  public getCart(id: string) {
    return this.http.get(this.baseUrl + 'cart?id=' + id).pipe(
      map((cart: ICart) => {
        if (cart.items.length !== 0)
          this.cartSource.next(cart);
        else
          this.deleteCart(cart);
        if (cart.deliveryMethodId !== null) {
          this.getDeliveryMethod(cart.deliveryMethodId).subscribe(
            (res) => {
              this.shipping = res.price;
            },
            (err) => {
              console.error(err);
            },
            () => {
              this.calculateTotals();
            }
          );
        }
        else {
          this.calculateTotals();
        }
      })
    );
  }

  public setCart(cart: ICart) {
    return this.http.post(this.baseUrl + 'cart', cart).subscribe(
      (response: ICart) => {
        this.cartSource.next(response);
        this.calculateTotals();
      },
      error => {
        console.log(error);
      }
    );
  }

  public deleteCart(cart: ICart) {
    this.shipping = 0.0;
    return this.http.delete(this.baseUrl + 'cart?id=' + cart.id).subscribe(() => {
      this.cartSource.next(null);
      this.cartTotalSource.next(null);
      localStorage.removeItem('cart-id');
    }, error => {
      console.log(error);
    });
  }

  // // tslint:disable-next-line: typedef
  // public deleteLocalBasket() {
  //   this.basketSource.next(null);
  //   this.basketTotalSource.next(null);
  //   this.shipping = 0.0;
  //   localStorage.removeItem('basket-id');
  // }

  public addItemToCart(item: IProduct, quantity = 1) {
    this.shopService.getProduct(item.id).subscribe(
      (res) => {
        if (res.isInStock) {
          const cart = this.getCurrentCartValue() ?? this.createCart();
          const itemToAdd: ICartItem = this.mapProductToCartITem(item, quantity);
          const currentItem = cart.items.find(x => x.id === itemToAdd.id)
          if (quantity > res.quantity || (currentItem && currentItem.quantity >= res.quantity)) {
            this.toastr.error("This quantity is greater than the current stock of this product");
          }
          else {
            cart.items = this.addOrUpdateItem(cart.items, itemToAdd, quantity);
            this.setCart(cart);
            this.toastr.info("Product added to your cart!");
          }
        }
        else
          this.toastr.error("This product is no longer in stock.", "Not in stock!");
      }
    );
  }

  public addItemToCartWithNoChecks(item: IProduct) {
    if (item.isInStock) {
      const cart = this.getCurrentCartValue() ?? this.createCart();
      const itemToAdd: ICartItem = this.mapProductToCartITem(item, 1);
      cart.items = this.addOrUpdateItem(cart.items, itemToAdd, 1);
      return this.setCart(cart);
    }
    else
      this.toastr.error("This product is no longer in stock.", "Not in stock!");
  }

  public incrementItemQuantity(item: ICartItem): void {
    this.shopService.getProduct(item.id).subscribe(
      (res) => {
        if (item.quantity >= res.quantity || !item.isInStock)
          this.toastr.error("This quantity is greater than the current stock of this product");
        else {
          const cart = this.getCurrentCartValue();
          const foundItem = cart.items.findIndex((x) => x.id === item.id);
          cart.items[foundItem].quantity++;
          this.setCart(cart);
        }
      }
    );
  }

  public decrementItemQuantity(item: ICartItem): void {
    const cart = this.getCurrentCartValue();
    const foundItem = cart.items.findIndex((x) => x.id === item.id);
    if (cart.items[foundItem].quantity > 1) {
      cart.items[foundItem].quantity--;
      this.setCart(cart);
    } else {
      this.removeItem(item);
    }
  }

  public getCurrentCartValue(): ICart {
    return this.cartSource.getValue();
  }

  public removeItem(item: ICartItem): void {
    const cart = this.getCurrentCartValue();
    if (cart.items.some(x => x.id === item.id)) {
      cart.items = cart.items.filter(i => i.id !== item.id);

      if (cart.items.length > 0) {
        this.setCart(cart);
      }
      else {
        this.deleteCart(cart);
      }
    }
  }

  private createCart(): ICart {
    const cart = new Cart();
    localStorage.setItem('cart-id', cart.id);
    return cart;
  }

  private getDeliveryMethod(id: number) {
    return this.http.get<IDeliveryMethod>(this.baseUrl + 'order/deliveryMethod/' + id);
  }

  private mapProductToCartITem(item: IProduct, quantity: number): ICartItem {
    return {
      id: item.id,
      productName: item.name,
      price: item.price,
      pictureUrl: item.pictureUrl,
      category: item.productCategory,
      type: item.productType,
      isInStock: item.isInStock,
      quantity,
    };
  }

  private addOrUpdateItem(items: ICartItem[], itemToAdd: ICartItem, quantity: number): ICartItem[] {
    const index = items.findIndex(i => i.id === itemToAdd.id);

    if (index === -1) {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    } else {
      items[index].quantity += quantity;
    }

    return items;
  }

  private calculateTotals(): void {
    const cart = this.getCurrentCartValue();
    const subtotal = cart.items.reduce((value, item) => (item.quantity * item.price) + value, 0);
    const shipping = this.shipping;

    const total = subtotal + shipping;
    this.cartTotalSource.next({
      shipping,
      total,
      subtotal,
    });
  }
}
