import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';
import { NgbCollapseModule, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '../shared/shared.module';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServerErrorComponent } from './server-error/server-error.component';

@NgModule({
  declarations: [
    NavbarComponent,
    FooterComponent,
    NotFoundComponent,
    ServerErrorComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    NgbModule,
    SharedModule
  ],
  
  exports: [
    NavbarComponent,
    FooterComponent
  ]
})
export class CoreModule { }
