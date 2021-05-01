import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { CartService } from 'src/app/modules/cart/cart.service';
import { ICart, ICartItem } from '../../models/Cart';

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.scss']
})
export class SummaryComponent implements OnInit {
  cart$: Observable<ICart>;
  @Input() isFunctionalityEnabled = true;
  @Output() decrement: EventEmitter<ICartItem> = new EventEmitter<ICartItem>();
  @Output() increment: EventEmitter<ICartItem> = new EventEmitter<ICartItem>();
  @Output() delete: EventEmitter<ICartItem> = new EventEmitter<ICartItem>();

  constructor(private basketService: CartService) { }

  ngOnInit(): void {
    this.cart$ = this.basketService.cart$;
  }

  decrementItem(item: ICartItem): void{
    this.decrement.emit(item);
  }

  incrementItem(item: ICartItem): void{
    this.increment.emit(item);
  }

  deleteItem(item: ICartItem): void{
    this.delete.emit(item);
  }
}
