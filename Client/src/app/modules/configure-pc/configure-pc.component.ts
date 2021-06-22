import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { IProduct } from 'src/app/shared/models/IProduct';
import { CartService } from '../cart/cart.service';

@Component({
  selector: 'app-configure-pc',
  templateUrl: './configure-pc.component.html',
  styleUrls: ['./configure-pc.component.scss']
})
export class ConfigurePcComponent implements OnInit {
  @Output() motherboardSelected = new EventEmitter();
  @Output() ramCpuSelected = new EventEmitter();

  public isValid: boolean[];
  selectedMotherboard: IProduct;
  selectedCpu: IProduct;
  selectedRam: IProduct;
  selectedHdd: IProduct;
  selectedGpu: IProduct;
  selectedPowerSupply: IProduct;

  constructor(private cartService: CartService) { }

  ngOnInit(): void {
    this.isValid = new Array(6).fill(false);
  }

  addItemToCart(item: IProduct): void {
   this.cartService.addItemToCart(item);
  }
}
