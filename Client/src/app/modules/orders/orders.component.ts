import { Component, OnInit } from '@angular/core';
import { IPagination } from 'src/app/shared/models/IPagination';
import { IOrder } from 'src/app/shared/models/Order';
import { OrderParams } from 'src/app/shared/models/Params';
import { OrdersService } from './orders.service';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent implements OnInit {
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
    this.getUserOrders();
  }

  getUserOrders(): void {
    this.orderParams.pageSize = this.pageSize;
    this.ordersService.getOrders(this.orderParams).subscribe(
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
      this.getUserOrders();
    }
  }

  public OnPageSize(pageSize: number): void {
    this.pageSize = pageSize;
    this.orderParams.pageNumber = 1;
    this.getUserOrders();
  }
  
  public OnSortSelected(sort: string): void {
    this.orderParams.sort = sort;
    this.orderParams.pageNumber = 1;
    this.getUserOrders();
  }
}
