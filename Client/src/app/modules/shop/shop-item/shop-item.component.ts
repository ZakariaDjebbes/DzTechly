import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/IProduct';
import { CartService } from '../../cart/cart.service';

@Component({
  selector: 'app-shop-item',
  templateUrl: './shop-item.component.html',
  styleUrls: ['./shop-item.component.scss']
})
export class ShopItemComponent implements OnInit {
  @Input() product: IProduct;

  constructor(private cartService: CartService) { }

  ngOnInit(): void {
  }
  
  public addItemToCart(): void {
    this.cartService.addItemToCart(this.product);
  }
}
