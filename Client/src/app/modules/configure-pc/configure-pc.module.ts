import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfigurePcComponent } from './configure-pc.component';
import { ConfigurePcRoutingModule } from './configure-pc-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [ConfigurePcComponent],
  imports: [
    CommonModule,
    ConfigurePcRoutingModule,
    SharedModule
  ]
})
export class ConfigurePcModule { }
