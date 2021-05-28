import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { IDeliveryMethod } from 'src/app/shared/models/IDeliveryMethod';
import { IOrderToCreate } from 'src/app/shared/models/Order';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  public getDeliveryMethods() {
    return this.http.get(this.baseUrl + 'order/deliveryMethods').pipe(
      map((dms: IDeliveryMethod[]) => {
        return dms.sort((a, b) => b.price - a.price);
      }, (error) => {
        console.error(error);
      })
    );
  }

  // tslint:disable-next-line: typedef
  public createOrder(order: IOrderToCreate) {
    return this.http.post(this.baseUrl + 'order', order);
  }
}
