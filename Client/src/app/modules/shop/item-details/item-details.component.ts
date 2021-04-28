import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct, IProductInfo } from 'src/app/shared/models/IProduct';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-item-details',
  templateUrl: './item-details.component.html',
  styleUrls: ['./item-details.component.scss']
})
export class ItemDetailsComponent implements OnInit {
  product: IProduct;
  categories: Array<[string, IProductInfo]>;
  quantity = 1;

  constructor(private shopService: ShopService,
    private activatedRoot: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.loadPrudct();
  }

  incrementQuantity(): void {
    this.quantity++;
  }

  decrementQuantity(): void {
    if (this.quantity > 1) { this.quantity--; }
  }

  loadPrudct(): void {
    this.shopService.getProduct(+this.activatedRoot.snapshot.paramMap.get('id')).subscribe(prodcut => {
      this.product = prodcut;
      this.categories = Object.entries(prodcut.technicalSheet.productAddtionalInfos); 
      console.log(this.categories)
    }, error => {
      console.error(error);
    });
  }
}
