import { CdkStepper } from '@angular/cdk/stepper';
import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Observable } from 'rxjs';
import { ICart } from 'src/app/shared/models/Cart';
import { IAddress } from 'src/app/shared/models/IAddress';
import { IPersonalInformation } from 'src/app/shared/models/IPersonalInformation';
import { CartService } from '../../cart/cart.service';

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html',
  styleUrls: ['./checkout-review.component.scss']
})
export class CheckoutReviewComponent implements OnInit {
  @Input() cdkStepper: CdkStepper;

  personalInfo: IPersonalInformation;
  address: IAddress;

  loading = false;

  constructor(private cartService: CartService) { }

  ngOnInit(): void {
  }
  
  // tslint:disable-next-line: typedef
  createPaymentIntent()
  {
    this.loading = true;
    return this.cartService.createPaymentIntent().subscribe(
      (res) => {
        this.cdkStepper.next();
      },
      (err) => {
        console.error(err);
        this.loading = false;
      },
      () => {
        this.loading = false;
      }
    );
  }

  backToDeliery(): void
  {
    this.cdkStepper.previous();
  }
}
