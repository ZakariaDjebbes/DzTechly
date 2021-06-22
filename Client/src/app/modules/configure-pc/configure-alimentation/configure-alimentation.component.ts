import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/IProduct';
import { IProductType } from 'src/app/shared/models/IProductType';
import { ShopService } from '../../shop/shop.service';
import { ConfigurePcComponent } from '../configure-pc.component';
import { ConfigurePcService } from '../configure-pc.service';

@Component({
  selector: 'app-configure-alimentation',
  templateUrl: './configure-alimentation.component.html',
  styleUrls: ['./configure-alimentation.component.scss']
})
export class ConfigureAlimentationComponent implements OnInit {
  @Input() parent: ConfigurePcComponent;

  alims: IProduct[];
  types: IProductType[];

  constructor(private configurePcService: ConfigurePcService, private shopService: ShopService) { }

  ngOnInit(): void {
    this.parent.motherboardSelected.subscribe(
      () => {
        this.getTypes();
      }
    )
  }

  getAlims(): void {
    const typeId = this.types.filter(x => x.name === 'Power supply')[0].id;
    this.configurePcService.getProductsOfType(typeId).subscribe(
      (res) => {
        this.alims = res;
      },
      (err) => console.log(err))
  }

  private getTypes(): void {
    this.shopService.getTypes().subscribe(response => {
      this.types = response;
      this.getAlims();
    }, error => {
      console.error(error);
    });
  }

  OnChoose(item: IProduct): void {
    this.parent.selectedPowerSupply = item;

    if(!this.parent.isValid[3])
      this.parent.isValid[3] = true;
  }
}
