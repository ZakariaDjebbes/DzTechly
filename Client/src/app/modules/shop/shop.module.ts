import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { ShopRoutingModule } from './shop-routing.module';
import { ShopItemComponent } from './shop-item/shop-item.component';
import { ProductReviewComponent } from './product-review/product-review.component';
import { ItemDetailsComponent } from './item-details/item-details.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [ShopComponent, ShopItemComponent, ProductReviewComponent, ItemDetailsComponent],
  imports: [
    CommonModule,
    SharedModule,
    ShopRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports:[
    ShopComponent,
    ItemDetailsComponent
  ]
})
export class ShopModule {

}
