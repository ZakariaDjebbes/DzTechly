import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbCollapseModule, NgbNavModule, NgbPaginationModule, NgbRatingModule } from '@ng-bootstrap/ng-bootstrap';
import { PagerComponent } from './components/pager/pager.component';
import { TextInputComponent } from './components/text-input/text-input.component';

@NgModule({
  declarations: [PagerComponent, TextInputComponent],
  imports: [
    CommonModule,
    NgbRatingModule,
    NgbNavModule,
    NgbPaginationModule,
    NgbCollapseModule
  ],
  exports:[
    NgbRatingModule,
    NgbNavModule,
    NgbPaginationModule,
    PagerComponent,
    NgbCollapseModule,
    TextInputComponent
  ]
})
export class SharedModule { }
