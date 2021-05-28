import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { IPagination } from 'src/app/shared/models/IPagination';
import { IOrder } from 'src/app/shared/models/Order';
import { OrderParams } from 'src/app/shared/models/Params';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  // tslint:disable-next-line: typedef
  public getOrders(orderParams: OrderParams) {
    let params = new HttpParams();

    params = params.append('sort', orderParams.sort);
    params = params.append('pageIndex', orderParams.pageNumber.toString());
    params = params.append('pageSize', orderParams.pageSize.toString());
    return this.http.get<IPagination<IOrder>>(this.baseUrl + 'order', { observe: 'response', params })
    .pipe(
      map(response => {
        return response.body;
      })
    );
  }

  // tslint:disable-next-line: typedef
  public getOrder(orderId: number) {
    return this.http.get<IOrder>(this.baseUrl + `order/${orderId}`);
  }
}
