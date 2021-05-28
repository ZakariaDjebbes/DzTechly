import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IOrder } from 'src/app/shared/models/Order';
import { OrdersService } from '../orders.service';

@Component({
  selector: 'app-order-detail',
  templateUrl: './order-detail.component.html',
  styleUrls: ['./order-detail.component.scss']
})
export class OrderDetailComponent implements OnInit {
  order: IOrder;

  constructor(private activatedRoot: ActivatedRoute, private orderService: OrdersService) {
  }

  ngOnInit(): void {
    this.getOrder();
  }

  private getOrder(): void{
    this.orderService.getOrder(+this.activatedRoot.snapshot.paramMap.get('id')).subscribe(
      (res) => {
        this.order = res;
        console.log(this.order);
      },
      (error) => {
        console.error(error);
      }
    );
  }
}
