import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrdersService } from 'src/app/modules/orders/orders.service';
import { IOrder } from 'src/app/shared/models/Order';

@Component({
  selector: 'app-order-history-details',
  templateUrl: './order-history-details.component.html',
  styleUrls: ['./order-history-details.component.scss']
})
export class OrderHistoryDetailsComponent implements OnInit {
  order: IOrder;

  constructor(private activatedRoot: ActivatedRoute, private orderService: OrdersService) {
  }

  ngOnInit(): void {
    this.getOrder();
  }

  private getOrder(): void {
    this.orderService.getAdministrationOrder(+this.activatedRoot.snapshot.paramMap.get('id')).subscribe(
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
