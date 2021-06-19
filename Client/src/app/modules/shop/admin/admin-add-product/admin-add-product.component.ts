import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ShopComponent } from '../../shop.component';
import { AddProductModalComponent } from '../add-product-modal/add-product-modal.component';

@Component({
  selector: 'app-admin-add-product',
  templateUrl: './admin-add-product.component.html',
  styleUrls: ['./admin-add-product.component.scss']
})
export class AdminAddProductComponent implements OnInit {
  @Input() shopComponent: ShopComponent;

  constructor(private modalService: NgbModal) { }

  ngOnInit(): void {
  }


  open(): void {
    const modalRef = this.modalService.open(AddProductModalComponent, {
      size: 'xl',
    });

    modalRef.componentInstance.shopComponent = this.shopComponent;
  }
}
