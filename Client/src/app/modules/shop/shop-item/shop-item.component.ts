import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IProduct } from 'src/app/shared/models/IProduct';
import { CartService } from '../../cart/cart.service';
import { DeleteProductModalComponent } from '../admin/delete-product-modal/delete-product-modal.component';
import { UpdateProductModalComponent } from '../admin/update-product-modal/update-product-modal.component';
import { ShopComponent } from '../shop.component';

@Component({
  selector: 'app-shop-item',
  templateUrl: './shop-item.component.html',
  styleUrls: ['./shop-item.component.scss']
})
export class ShopItemComponent implements OnInit {
  @Input() shopComponent: ShopComponent;
  @Input() product: IProduct;

  constructor(private cartService: CartService, private modalService: NgbModal) { }

  ngOnInit(): void {
  }
  
  public addItemToCart(): void {
    this.cartService.addItemToCart(this.product);
  }

  openDeleteModal(): void {
    const modalRef = this.modalService.open(DeleteProductModalComponent, {
      size: 'md',
    });

    modalRef.componentInstance.shopComponent = this.shopComponent;
    modalRef.componentInstance.product = this.product;
  }

  openUpdateModal(): void {
    const modalRef = this.modalService.open(UpdateProductModalComponent, {
      size: 'xl',
    });

    modalRef.componentInstance.shopComponent = this.shopComponent;
    modalRef.componentInstance.product = this.product;
  }
}
