import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';
import {RequestEmailConfirmationComponent } from './request-email-confirmation/request-email-confirmation.component'
import {RequestResetPasswordComponent } from './request-reset-password/request-reset-password.component'
import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { ProfileComponent } from './profile/profile.component';
import { RequestSuccessComponent } from './request-success/request-success.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';



@NgModule({
  declarations: [LoginComponent, RegisterComponent, ConfirmEmailComponent, ProfileComponent, RequestEmailConfirmationComponent, RequestResetPasswordComponent, RequestSuccessComponent, ResetPasswordComponent],
  imports: [
    CommonModule,
    AccountRoutingModule,
    SharedModule
  ],
  exports:[
    LoginComponent,
    RegisterComponent
  ]
})
export class AccountModule { }
