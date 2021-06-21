import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { IProduct } from 'src/app/shared/models/IProduct';
import { ProductReviewComponent } from '../../product-review/product-review.component';
import { ShopService } from '../../shop.service';

@Component({
  selector: 'app-delete-review-modal',
  templateUrl: './delete-review-modal.component.html',
  styleUrls: ['./delete-review-modal.component.scss']
})
export class DeleteReviewModalComponent implements OnInit {
  product: IProduct;
  loading = false;
  id: number;
  reviewsComponent: ProductReviewComponent;

  constructor(public activeModal: NgbActiveModal, private shopService: ShopService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  public deleteReview(): void {
    this.loading = true;
    this.shopService.deleteReview(this.id).subscribe(
      (res) => {
        this.toastr.success("Review deleted with success");
        this.reviewsComponent.getReviews();
        this.activeModal.close();
        this.loading = false;
      },
      err => {
        this.toastr.error(err.message);
        this.loading = false;
      }
    );
  }

}
