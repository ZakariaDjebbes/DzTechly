import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { IDeliveryMethod } from 'src/app/shared/models/IDeliveryMethod';
import { CartService } from '../../cart/cart.service';
import { CheckoutService } from '../checkout.service';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.scss']
})
export class CheckoutDeliveryComponent implements OnInit {
  @Input() checkoutForm: FormGroup;
  deliveryMethods: IDeliveryMethod[];
  constructor(private checkoutService: CheckoutService, private cartService: CartService) { }

  ngOnInit(): void {
    this.checkoutService.getDeliveryMethods().subscribe((dms: IDeliveryMethod[]) => {
      this.deliveryMethods = dms;
    }, (error) => {
      console.error(error);
    });
  }

  setShippingPrice(dm: IDeliveryMethod): void {
    this.cartService.setShippingPrice(dm);
  }
}
