import { Component, OnInit } from '@angular/core';
import { IOrder } from 'src/app/shared/models/Order';
import { OrderParams } from 'src/app/shared/models/Params';
import { OrdersService } from '../../orders/orders.service';

@Component({
  selector: 'app-orders-history',
  templateUrl: './orders-history.component.html',
  styleUrls: ['./orders-history.component.scss']
})
export class OrdersHistoryComponent implements OnInit {
  orders: IOrder[];
  orderParams = new OrderParams();
  totalCount: number;
  pageSize = 6;
  pageSizes = [ 6, 15, 25, 50];

  sortOptions = [
    { name: 'Date: Latest first', value: 'dateDesc' },
    { name: 'Date:  Oldest first', value: 'dateAsc' },
    { name: 'Id: Low to high', value: 'idAsc' },
    { name: 'Id: High to low', value: 'idDesc' }
  ];

  constructor(private ordersService: OrdersService) { }

  ngOnInit(): void {
    this.getOrders();
  }

  getOrders(): void {
    this.orderParams.pageSize = this.pageSize;
    this.ordersService.getAllOrders(this.orderParams).subscribe(
      (res) => {
          this.orders = res.data;
          this.orderParams.pageNumber = res.pageIndex;
          this.orderParams.pageSize = res.pageSize;
          this.totalCount = res.count;
      }, (error) => {
        console.error(error);
      });
  }

  public OnPageChanged(event: any): void {
    if (this.orderParams.pageNumber !== event) {
      this.orderParams.pageNumber = event;
      this.getOrders();
    }
  }

  public OnPageSize(pageSize: number): void {
    this.pageSize = pageSize;
    this.orderParams.pageNumber = 1;
    this.getOrders();
  }
  
  public OnSortSelected(sort: string): void {
    this.orderParams.sort = sort;
    this.orderParams.pageNumber = 1;
    this.getOrders();
  }
}
