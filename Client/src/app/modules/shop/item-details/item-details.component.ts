import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Observable } from 'rxjs';
import { IProduct, IProductInfo } from 'src/app/shared/models/IProduct';
import { IUser } from 'src/app/shared/models/IUser';
import { AccountService } from '../../account/account.service';
import { CartService } from '../../cart/cart.service';
import { DeleteProductModalComponent } from '../admin/delete-product-modal/delete-product-modal.component';
import { UpdateProductModalComponent } from '../admin/update-product-modal/update-product-modal.component';
import { ShopComponent } from '../shop.component';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-item-details',
  templateUrl: './item-details.component.html',
  styleUrls: ['./item-details.component.scss']
})
export class ItemDetailsComponent implements OnInit {
  @Input() shopComponent: ShopComponent;

  product: IProduct;
  categories: Array<[string, IProductInfo]>;
  quantity = 1;
  user$: Observable<IUser>;
  loading = false;

  constructor(private shopService: ShopService, private activatedRoot: ActivatedRoute,
    private cartService: CartService, private modalService: NgbModal, private router: Router, private accountService: AccountService) {
  }

  ngOnInit(): void {
    this.loadPrudct();
    this.user$ = this.accountService.currentUser$;
  }

  addItemToCart(): void {
    this.cartService.addItemToCart(this.product, this.quantity);
  }

  incrementQuantity(): void {
    this.quantity++;
  }

  decrementQuantity(): void {
    if (this.quantity > 1) { this.quantity--; }
  }

  addToList(): void {
    this.loading = true;
    this.shopService.addToWaitingList(this.product.id).subscribe(
      (res: IProduct) => {
        this.loading = false;
        this.product = res;
      },
      (err) => {
        this.loading = false;
      }
    );
  }

  loadPrudct(): void {
    this.shopService.getProduct(+this.activatedRoot.snapshot.paramMap.get('id')).subscribe(prodcut => {
      this.product = prodcut;
      this.categories = Object.entries(prodcut.technicalSheet.productAddtionalInfos);
    }, error => {
      console.error(error);
    });
  }

  openDeleteModal(): void {
    const modalRef = this.modalService.open(DeleteProductModalComponent, {
      size: 'md',
    });

    modalRef.componentInstance.shopComponent = this.shopComponent;
    modalRef.componentInstance.product = this.product;

    modalRef.result.then(res => {
      if (res === true)
        this.router.navigateByUrl("/shop");
    }, (reason) => {
    });
  }

  openUpdateModal(): void {
    const modalRef = this.modalService.open(UpdateProductModalComponent, {
      size: 'xl',
    });

    modalRef.componentInstance.shopComponent = this.shopComponent;
    modalRef.componentInstance.product = this.product;
    modalRef.componentInstance.itemDetails = this;
  }
}
