import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { UnauthGuard } from 'src/app/core/guards/unauth.guard';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';
import { LoginComponent } from './login/login.component';
import { ProfileComponent } from './profile/profile.component';
import { RegisterComponent } from './register/register.component';
import { RequestEmailConfirmationComponent } from './request-email-confirmation/request-email-confirmation.component';
import { RequestResetPasswordComponent } from './request-reset-password/request-reset-password.component';
import { RequestSuccessComponent } from './request-success/request-success.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent, canActivate: [UnauthGuard] },
  { path: 'register', component: RegisterComponent, canActivate: [UnauthGuard] },
  { path: 'confirmEmail', component: ConfirmEmailComponent },
  { path: 'requestEmailConfirmation', component: RequestEmailConfirmationComponent },
  { path: 'requestSuccess', component: RequestSuccessComponent },
  { path: 'requestResetPassword', component: RequestResetPasswordComponent },
  { path: 'resetPassword', component: ResetPasswordComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] }
]

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AccountRoutingModule { }
