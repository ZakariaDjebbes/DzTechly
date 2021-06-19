import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { FooterComponent } from './footer/footer.component';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '../shared/shared.module';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { HasRoleDirective } from './directives/has-role.directive';

@NgModule({
  declarations: [
    NavbarComponent,
    FooterComponent,
    NotFoundComponent,
    ServerErrorComponent,
    HasRoleDirective
  ],
  imports: [
    CommonModule,
    RouterModule,
    NgbModule,
    SharedModule
  ],
  
  exports: [
    NavbarComponent,
    FooterComponent,
    HasRoleDirective
  ]
})
export class CoreModule { }
