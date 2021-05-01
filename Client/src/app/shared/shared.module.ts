import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbCollapseModule, NgbNavModule, NgbPaginationModule, NgbRatingModule } from '@ng-bootstrap/ng-bootstrap';
import { PagerComponent } from './components/pager/pager.component';
import { TextInputComponent } from './components/text-input/text-input.component';
import { TotalsComponent } from './components/totals/totals.component';
import { SummaryComponent } from './components/summary/summary.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [PagerComponent, TextInputComponent, TotalsComponent, SummaryComponent],
  imports: [
    CommonModule,
    NgbRatingModule,
    NgbNavModule,
    NgbPaginationModule,
    NgbCollapseModule,
    RouterModule
  ],
  exports:[
    NgbRatingModule,
    NgbNavModule,
    NgbPaginationModule,
    PagerComponent,
    NgbCollapseModule,
    TextInputComponent,
    TotalsComponent,
    SummaryComponent
  ]
})
export class SharedModule { }
