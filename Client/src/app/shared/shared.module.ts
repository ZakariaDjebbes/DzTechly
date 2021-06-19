import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CdkStepperModule } from '@angular/cdk/stepper';
import { NgbAccordionModule, NgbCollapseModule, NgbModalModule, NgbNavModule, NgbPaginationModule, NgbRatingModule } from '@ng-bootstrap/ng-bootstrap';
import { PagerComponent } from './components/pager/pager.component';
import { TextInputComponent } from './components/text-input/text-input.component';
import { TotalsComponent } from './components/totals/totals.component';
import { SummaryComponent } from './components/summary/summary.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormErrorsComponent } from './components/form-errors/form-errors.component';
import { StepperComponent } from './components/stepper/stepper.component';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { StatusBadgeComponent } from './components/status-badge/status-badge.component';

@NgModule({
  declarations: [PagerComponent, TextInputComponent, TotalsComponent, SummaryComponent, FormErrorsComponent, StepperComponent, PagingHeaderComponent, StatusBadgeComponent],
  imports: [
    CommonModule,
    NgbRatingModule,
    NgbNavModule,
    NgbPaginationModule,
    NgbCollapseModule,
    NgbModalModule,
    NgbAccordionModule,
    ReactiveFormsModule,
    RouterModule,
    CdkStepperModule,
    FormsModule,
  ],
  exports: [
    NgbRatingModule,
    NgbNavModule,
    NgbPaginationModule,
    NgbModalModule,
    NgbAccordionModule,
    FormsModule,
    CdkStepperModule,
    ReactiveFormsModule,
    PagerComponent,
    NgbCollapseModule,
    StatusBadgeComponent,
    TextInputComponent,
    FormErrorsComponent,
    TotalsComponent,
    SummaryComponent,
    StepperComponent,
    PagingHeaderComponent
  ]
})
export class SharedModule { }
