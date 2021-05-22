import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from 'src/app/modules/account/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private accountService: AccountService, private router: Router, private toastr: ToastrService) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map((user) => {
          if (user) {
            const fs = route.firstChild;
            const roles = fs ? fs.data.roles : null;
            if (roles)
            {
              const match = this.accountService.checkRoles(roles, user);

              if (match) {
                return true;
              }

              this.toastr.error('Allowed in this area, you are not', 'Error 403');
            }
            else
            {
              return true;
            }
          }
          else
          {
            this.router.navigate(['account/login'], {queryParams: {returnUrl: state.url}});
            this.toastr.error('You must be logged in to access this area', 'Error 401');
          }
      })
    );
  }
}
