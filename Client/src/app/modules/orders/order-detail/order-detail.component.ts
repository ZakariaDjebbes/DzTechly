import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IOrder } from 'src/app/shared/models/Order';
import { OrdersService } from '../orders.service';
import * as html2pdf from 'html2pdf.js'

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
      },
      (error) => {
        console.error(error);
      }
    );
  }

  getPdf(): void {
    const options = {
      filename: `Order#${this.order.id}#${this.order.paymentIntentId}-${this.order.status}`,
      image: {type: "jpg"},
      html2canvas: {},
      jsPDF: {orientation: "portrait"}
    }
    
    const content = document.getElementById("content");
    html2pdf()
    .from(content)
    .set(options)
    .save();
  }
}
