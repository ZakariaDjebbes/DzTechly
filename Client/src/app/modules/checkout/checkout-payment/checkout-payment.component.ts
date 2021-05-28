import { AfterViewInit, Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ICart } from 'src/app/shared/models/Cart';
import { IOrderToCreate } from 'src/app/shared/models/Order';
import { environment } from 'src/environments/environment';
import { CartService } from '../../cart/cart.service';
import { CheckoutService } from '../checkout.service';

declare let Stripe;

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements AfterViewInit, OnDestroy {
  @Input() checkoutForm: FormGroup;
  @ViewChild('cardNumber', {static: true}) cardNumberElement: ElementRef;
  @ViewChild('cardExpiry', {static: true}) cardExpiryElement: ElementRef;
  @ViewChild('cardCvc', {static: true}) cardCvcElement: ElementRef;

  stripe: any;
  cardNumber: any;
  cardExpiry: any;
  cardCvc: any;
  cardErrors: any;
  cardHandler = this.onChange.bind(this);
  loading = false;

  constructor(
    private cartService: CartService,
    private checkoutService: CheckoutService,
    private toastr: ToastrService,
    private router: Router) { }

  ngOnDestroy(): void {
    this.cardNumber.destroy();
    this.cardExpiry.destroy();
    this.cardCvc.destroy();
  }

  ngAfterViewInit(): void {
    this.setStripeElements();
  }

  // tslint:disable-next-line: typedef
  async submitOrder() {
    this.loading = true;
    const cart = this.cartService.getCurrentCartValue();
    try
    {
      const createdOrder = await this.createOrder(cart);
      const paymentResult = await this.confirmStripePayment(cart);

      if (paymentResult.paymentIntent) {
        this.cartService.deleteCart(cart);
        const navigationExtras: NavigationExtras = {state: createdOrder};
        this.router.navigate(['checkout/success'], navigationExtras);
      } else {
        this.toastr.error( paymentResult.error.message );
      }

    }catch (error)
    {
      console.error(error);
    }

    this.loading = false;
  }

  // tslint:disable-next-line: typedef
  private async confirmStripePayment(cart: ICart) {
    return this.stripe.confirmCardPayment(cart.clientSecret, {
      payment_method: {
        card: this.cardNumber,
        billing_details: {
          name: this.checkoutForm.get('paymentForm').get('nameOnCard').value
        }
      }
    });
  }

  // tslint:disable-next-line: typedef
  private async createOrder(cart: ICart) {
    const orderToCreate = this.getOrderToCreate(cart);
    return this.checkoutService.createOrder(orderToCreate).toPromise();
  }

  onChange({error}): void
  {
    if (error)
    {
      this.cardErrors = error.message;
    }
    else
    {
      this.cardErrors = null;
    }
  }

  private getOrderToCreate(cart: ICart): IOrderToCreate {
    return {
      cartId: cart.id,
      deliveryMethodId: +this.checkoutForm.get('deliveryForm').get('deliveryMethod').value,
      shippingAddress: this.checkoutForm.get('addressForm').value,
      personalInformation: this.checkoutForm.get('personalInformationForm').value
    };
  }

  private setStripeElements(): void
  {
    const stripePublicKey = environment.PublishibleKey;

    this.stripe = Stripe(stripePublicKey);
    const elements = this.stripe.elements();

    this.cardNumber = elements.create('cardNumber');
    this.cardNumber.mount(this.cardNumberElement.nativeElement);
    this.cardNumber.addEventListener('change', this.cardHandler);

    this.cardExpiry = elements.create('cardExpiry');
    this.cardExpiry.mount(this.cardExpiryElement.nativeElement);
    this.cardExpiry.addEventListener('change', this.cardHandler);

    this.cardCvc = elements.create('cardCvc');
    this.cardCvc.mount(this.cardCvcElement.nativeElement);
    this.cardCvc.addEventListener('change', this.cardHandler);
  }
}
