import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/IProduct';
import { IProductType } from 'src/app/shared/models/IProductType';
import { ShopService } from '../../shop/shop.service';
import { ConfigurePcComponent } from '../configure-pc.component';
import { ConfigurePcService } from '../configure-pc.service';

@Component({
  selector: 'app-configure-gpu-hdd',
  templateUrl: './configure-gpu-hdd.component.html',
  styleUrls: ['./configure-gpu-hdd.component.scss']
})
export class ConfigureGpuHddComponent implements OnInit {
  @Input() parent: ConfigurePcComponent;
  
  types: IProductType[];
  hdds: IProduct[];
  gpus: IProduct[] = [];

  constructor(private configurePcService: ConfigurePcService, private shopService: ShopService) { }

  ngOnInit(): void {
    this.parent.motherboardSelected.subscribe(
      () => {
        this.getTypes();
        this.getGpus();
      }
    )
  }


  getHdds(): void {
    const typeId = this.types.filter(x => x.name === 'Hard Drive')[0].id;
    this.configurePcService.getProductsOfType(typeId).subscribe(
      (res) => {
        this.hdds = res;
      },
      (err) => console.log(err))
  }

  getGpus(): void {
    this.configurePcService.getGpus(this.parent.selectedMotherboard.id).subscribe(
      (res) => {
        this.gpus = res;
      },
      (err) => console.log(err))
  }

  private getTypes(): void {
    this.shopService.getTypes().subscribe(response => {
      this.types = response;
      this.getHdds();
    }, error => {
      console.error(error);
    });
  }

  chooseGpu(item: IProduct): void {
    this.parent.selectedGpu = item;
  }

  chooseHdd(item: IProduct): void {
    this.parent.selectedHdd = item;

    if(!this.parent.isValid[2])
      this.parent.isValid[2] = true;
  }
}
