import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { IProduct } from 'src/app/shared/models/IProduct';
import { ShopComponent } from '../../shop.component';
import { ShopService } from '../../shop.service';

@Component({
  selector: 'app-delete-product-modal',
  templateUrl: './delete-product-modal.component.html',
  styleUrls: ['./delete-product-modal.component.scss']
})
export class DeleteProductModalComponent implements OnInit {
  shopComponent: ShopComponent;
  product: IProduct;
  loading = false;
  constructor(public activeModal: NgbActiveModal, private shopService: ShopService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  public deleteProduct(): void {
    this.loading = true;
    this.shopService.deleteProduct(this.product.id).subscribe(
      () => {
        this.toastr.success(`Product #${this.product.id} - ${this.product.name} has been correctly deleted.`);
        if (this.shopComponent)
          this.shopComponent.getProducts();
        this.activeModal.close(true);
        this.loading = false;
      },
      (error) => {
        this.toastr.error(error.meesage);
        this.loading = false;
      }
    );
  }
}
