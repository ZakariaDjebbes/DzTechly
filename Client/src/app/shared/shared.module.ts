import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbNavModule, NgbRatingModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    NgbRatingModule,
    NgbNavModule
  ],
  exports:[
    NgbRatingModule,
    NgbNavModule
  ]
})
export class SharedModule { }
