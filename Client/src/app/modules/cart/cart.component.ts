import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ICart, ICartItem } from 'src/app/shared/models/Cart';
import { CartService } from './cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  cart$: Observable<ICart>;

  constructor(private cartService: CartService) { }

  ngOnInit(): void {
    this.cart$ = this.cartService.cart$;
  }

  public removeItem(item: ICartItem): void {
    this.cartService.removeItem(item);
  }

  public incrementItem(item: ICartItem): void {
    this.cartService.incrementItemQuantity(item);
  }

  public decrementItem(item: ICartItem): void {
    this.cartService.decrementItemQuantity(item);
  }
}
