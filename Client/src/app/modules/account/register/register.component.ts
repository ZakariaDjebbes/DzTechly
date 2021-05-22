import { Component, OnInit } from '@angular/core';
import { AsyncValidatorFn, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { of, timer } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  errors: [];
  loading = false;

  constructor(private formBuilder: FormBuilder, private router: Router, private accountService: AccountService) { }

  ngOnInit(): void {
    this.createRegisterForm();
  }

  createRegisterForm(): void {
    this.registerForm = this.formBuilder.group({
      userName: [null, [Validators.required],
        [this.validateUserNotTaken()]],
      email: [null,
        [Validators.required, Validators.pattern('^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$')],
        [this.validateEmailNotTaken()]
      ],
      password: [null, [Validators.required]],
      confirmPassword: [null, [Validators.required]],
      personalInformation: this.formBuilder.group({
        firstName: [null, []],
        lastName: [null, []],
        birthDate: [null, []],
      }),
      address: this.formBuilder.group({
        wilaya: [null, []],
        city: [null, []],
        street: [null, []],
        zipcode: [null, []]
      }),
    }, { validators: this.passwordsMatchValidator });
  }

  onSubmit(): void {
    this.loading = true;
    this.accountService.register(this.registerForm.value).subscribe(
      (res) => {
        if (res) {
          const navigationExtras: NavigationExtras = { state: this.registerForm.get('email').value };
          this.router.navigate(['/account/requestSuccess'], navigationExtras);
        }
      },
      (error) => {
        console.error(error);
        this.errors = error.errors;
        this.loading = false;
      },
      () => {
        this.loading = false;
      }
    );
  }

  validateEmailNotTaken(): AsyncValidatorFn {
    return control => {
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value) { return of(null); }
          return this.accountService.checkEmailExists(control.value).pipe(map(res => res ? { emailExists: true } : null));
        })
      );
    };
  }

  validateUserNotTaken(): AsyncValidatorFn {
    return control => {
      return timer(500).pipe(
        switchMap(() => {
          if (!control.value) { return of(null); }
          return this.accountService.checkUserExists(control.value).pipe(map(res => res ? { userExists: true } : null));
        })
      );
    };
  }

  // tslint:disable-next-line: typedef
  private passwordsMatchValidator(form: FormGroup) {
    if (form.get('password') && form.get('confirmPassword')) {
      if (form.get('password').value === form.get('confirmPassword').value) {
        return null;
      }
      else {
        form.get('confirmPassword').setErrors({ mismatch: true });
        return { mismatch: true };
      }
    }
    return null;
  }
}
