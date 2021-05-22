import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbCollapseModule, NgbNavModule, NgbPaginationModule, NgbRatingModule } from '@ng-bootstrap/ng-bootstrap';
import { PagerComponent } from './components/pager/pager.component';
import { TextInputComponent } from './components/text-input/text-input.component';
import { TotalsComponent } from './components/totals/totals.component';
import { SummaryComponent } from './components/summary/summary.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormErrorsComponent } from './components/form-errors/form-errors.component';

@NgModule({
  declarations: [PagerComponent, TextInputComponent, TotalsComponent, SummaryComponent, FormErrorsComponent],
  imports: [
    CommonModule,
    NgbRatingModule,
    NgbNavModule,
    NgbPaginationModule,
    NgbCollapseModule,
    ReactiveFormsModule,
    RouterModule,
    FormsModule
  ],
  exports:[
    NgbRatingModule,
    NgbNavModule,
    NgbPaginationModule,
    FormsModule,
    ReactiveFormsModule,
    PagerComponent,
    NgbCollapseModule,
    TextInputComponent,
    FormErrorsComponent,
    TotalsComponent,
    SummaryComponent
  ]
})
export class SharedModule { }
