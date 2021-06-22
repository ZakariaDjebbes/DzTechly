import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/IProduct';
import { ConfigurePcComponent } from '../configure-pc.component';
import { ConfigurePcService } from '../configure-pc.service';

@Component({
  selector: 'app-configure-cpu-ram',
  templateUrl: './configure-cpu-ram.component.html',
  styleUrls: ['./configure-cpu-ram.component.scss']
})
export class ConfigureCpuRamComponent implements OnInit {
  @Input() parent: ConfigurePcComponent;

  cpus: IProduct[];
  rams: IProduct[];

  constructor(private configurePcService: ConfigurePcService) { }

  ngOnInit(): void {
    this.parent.motherboardSelected.subscribe(
      () => {
        this.getCpus();
        this.getRams();
      }
    )
  }

  getCpus(): void {
    this.configurePcService.getCpus(this.parent.selectedMotherboard.id).subscribe(
      (res) => {
        this.cpus = res;
      },
      (err) => console.log(err))
  }
  
  getRams(): void {
    this.configurePcService.getRams(this.parent.selectedMotherboard.id).subscribe(
      (res) => {
        this.rams = res;
      },
      (err) => console.log(err))
  }

  chooseCpu(item: IProduct): void {
    this.parent.selectedCpu = item;
  }

  chooseRam(item: IProduct): void {
    this.parent.selectedRam = item;

    if(!this.parent.isValid[1])
      this.parent.isValid[1] = true;

      this.parent.ramCpuSelected.emit();
  }
}
