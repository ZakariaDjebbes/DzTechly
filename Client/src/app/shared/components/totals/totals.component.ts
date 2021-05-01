import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CartService } from 'src/app/modules/cart/cart.service';
import { ICartTotals } from '../../models/Cart';

@Component({
  selector: 'app-order-totals',
  templateUrl: './totals.component.html',
  styleUrls: ['./totals.component.scss']
})
export class TotalsComponent implements OnInit {
  cartTotal$: Observable<ICartTotals>;
  constructor(private cartService: CartService) { }

  ngOnInit(): void {
    this.cartTotal$ = this.cartService.cartTotal$;
  }
}
