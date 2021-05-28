import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account/account.service';
import { CartService } from '../cart/cart.service';


@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {
  checkoutForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private accountService: AccountService, private cartService: CartService) { }

  ngOnInit(): void {
    this.createCheckoutForm();
    this.getAddressForValues();
    this.getPersonalInfoForValues();
    this.getDeliveryMethod();
  }

  createCheckoutForm(): void {
    this.checkoutForm = this.formBuilder.group(
      {
        personalInformationForm: this.formBuilder.group({
          firstName: [null, [Validators.required]],
          lastName: [null, [Validators.required]],
          birthDate: [null, []],
        }),
        addressForm: this.formBuilder.group({
          wilaya: [null, [Validators.required]],
          city: [null, [Validators.required]],
          street: [null, [Validators.required]],
          zipcode: [null, [Validators.required]]
        }),
        deliveryForm: this.formBuilder.group({
          deliveryMethod: [null, Validators.required]
        }),
        paymentForm: this.formBuilder.group({
          nameOnCard: [null, Validators.required]
        })
      }
    );
  }

  getAddressForValues(): void {
    this.accountService.getUserAddress().subscribe(address => {
      if (address)
      {
        this.checkoutForm.get('addressForm').patchValue(address);
      }
    }, (error) => {
      console.error(error);
    });
  }

  getPersonalInfoForValues(): void {
    this.accountService.getUserInfos().subscribe(info => {
      if (info)
      {
        this.checkoutForm.get('personalInformationForm').patchValue(info);
      }
    }, (error) => {
      console.error(error);
    });
  }

  getDeliveryMethod(): void {
    const cart = this.cartService.getCurrentCartValue();
    if (cart.deliveryMethodId)
    {
      this.checkoutForm.get('deliveryForm').get('deliveryMethod').patchValue(cart.deliveryMethodId.toString());
    }
  }
}
