import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfigurePcComponent } from './configure-pc.component';
import { ConfigurePcRoutingModule } from './configure-pc-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { ConfigureMotherboardComponent } from './configure-motherboard/configure-motherboard.component';
import { ConfigureCpuRamComponent } from './configure-cpu-ram/configure-cpu-ram.component';
import { ConfigureGpuHddComponent } from './configure-gpu-hdd/configure-gpu-hdd.component';
import { ConfigureAlimentationComponent } from './configure-alimentation/configure-alimentation.component';
import { ConfigureReviewComponent } from './configure-review/configure-review.component';

@NgModule({
  declarations: [ConfigurePcComponent, ConfigureMotherboardComponent, ConfigureCpuRamComponent, ConfigureGpuHddComponent, ConfigureAlimentationComponent, ConfigureReviewComponent],
  imports: [
    CommonModule,
    ConfigurePcRoutingModule,
    SharedModule
  ]
})
export class ConfigurePcModule { }
