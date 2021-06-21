import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { ShopRoutingModule } from './shop-routing.module';
import { ShopItemComponent } from './shop-item/shop-item.component';
import { ProductReviewComponent } from './product-review/product-review.component';
import { ItemDetailsComponent } from './item-details/item-details.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminAddProductComponent } from './admin/admin-add-product/admin-add-product.component';
import { CoreModule } from 'src/app/core/core.module';
import { AddProductModalComponent } from './admin/add-product-modal/add-product-modal.component';
import { AddCategoryModalComponent } from './admin/add-category-modal/add-category-modal.component';
import { AddInfoModalComponent } from './admin/add-info-modal/add-info-modal.component';
import { DeleteProductModalComponent } from './admin/delete-product-modal/delete-product-modal.component';
import { UpdateProductModalComponent } from './admin/update-product-modal/update-product-modal.component';
import { DeleteReviewModalComponent } from './admin/delete-review-modal/delete-review-modal.component';



@NgModule({
  declarations: [ShopComponent, ShopItemComponent, ProductReviewComponent, ItemDetailsComponent, AdminAddProductComponent, AddProductModalComponent, AddCategoryModalComponent, AddInfoModalComponent, DeleteProductModalComponent, UpdateProductModalComponent, DeleteReviewModalComponent],
  imports: [
    CommonModule,
    SharedModule,
    ShopRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    CoreModule
  ],
  exports:[
    ShopComponent,
    ItemDetailsComponent
  ]
})
export class ShopModule {

}
