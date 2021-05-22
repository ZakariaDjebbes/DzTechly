import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { of, timer } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-request-reset-password',
  templateUrl: './request-reset-password.component.html',
  styleUrls: ['./request-reset-password.component.scss']
})
export class RequestResetPasswordComponent implements OnInit {
  resetPasswordForm: FormGroup;
  loading = false;
  checking = false;

  constructor(private formBuilder: FormBuilder, private accountService: AccountService, private router: Router) { }

  ngOnInit(): void {
    this.createResetPasswordForm();
  }

  onSubmit(): void{
   this.loading = true;
   this.accountService.requestPasswordReset(this.resetPasswordForm.get('email').value).subscribe(
    (res) => {
      const navigationExtras: NavigationExtras = { state: this.resetPasswordForm.get('email').value };
      this.router.navigate(['/account/requestSuccess'], navigationExtras);
    },
    (error) => {
      console.error(error);
      this.loading = false;
    }
  );
  }

  createResetPasswordForm(): void
  {
    this.resetPasswordForm = this.formBuilder.group(
      {
        email: [null, [Validators.required], [this.validateEmailTaken()]]
      }
    );
  }

  validateEmailTaken(): any {
    this.checking = true;
    return control => {
      return timer(500).pipe(
        switchMap(() => {
          this.checking = false;
          if (!control.value) { return of(null); }
          return this.accountService.checkEmailExists(control.value).pipe(map(res => !res ? { emailNotExists: true } : null));
        })
      );
    };
  }
}
