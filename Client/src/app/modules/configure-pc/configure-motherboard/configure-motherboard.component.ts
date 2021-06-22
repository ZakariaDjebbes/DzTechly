import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/IProduct';
import { IProductType } from 'src/app/shared/models/IProductType';
import { ShopService } from '../../shop/shop.service';
import { ConfigurePcComponent } from '../configure-pc.component';
import { ConfigurePcService } from '../configure-pc.service';

@Component({
  selector: 'app-configure-motherboard',
  templateUrl: './configure-motherboard.component.html',
  styleUrls: ['./configure-motherboard.component.scss']
})
export class ConfigureMotherboardComponent implements OnInit {
  @Input() parent: ConfigurePcComponent;

  motherboards: IProduct[];
  types: IProductType[];
  isValid: boolean = false;

  constructor(private configurePcService: ConfigurePcService, private shopService: ShopService) { }

  ngOnInit(): void {
    this.getTypes();
  }

  private getAllMotherboards(): void {
    const typeId = this.types.filter(x => x.name === 'Motherboard')[0].id;
    this.configurePcService.getProductsOfType(typeId).subscribe(
      (res: IProduct[]) => {
        this.motherboards = res;
      },
      (err) => console.log(err)
    );
  }

  private getTypes(): void {
    this.shopService.getTypes().subscribe(response => {
      this.types = response;
      this.getAllMotherboards();
    }, error => {
      console.error(error);
    });
  }

  OnChoose(item: IProduct): void {
    this.parent.selectedMotherboard = item;

    if(!this.parent.isValid[0])
      this.parent.isValid[0] = true;

    this.parent.motherboardSelected.emit();
  }
}
