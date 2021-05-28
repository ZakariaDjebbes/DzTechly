import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { IUser } from 'src/app/shared/models/IUser';
import { environment } from 'src/environments/environment';
import jwtDecode from 'jwt-decode';
import { IAddress } from 'src/app/shared/models/IAddress';
import { IPersonalInformation } from 'src/app/shared/models/IPersonalInformation';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<IUser>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  public confirmEmail(token: string, email: string): Observable<boolean> {
    const params = {
      token,
      email
    };
    return this.http.get<boolean>(this.baseUrl + 'account/confirmEmail', { params });
  }

  public resetPassword(values: any): Observable<boolean> {
    return this.http.get<boolean>(this.baseUrl + 'account/resetPassword', { params: values });
  }

  // tslint:disable-next-line: typedef
  public login(values: any) {
    return this.http.post<IUser>(this.baseUrl + 'account/login', values)
      .pipe(map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
          return user;
        }
      }));
  }

  public logout(): void {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  public requestConfirmationEmail(email: string): Observable<any> {
    const params = {
      email
    };

    return this.http.get(this.baseUrl + 'account/requestConfirmationEmail', { params });
  }

  public requestPasswordReset(email: string): Observable<any> {
    const params = {
      email
    };

    return this.http.get(this.baseUrl + 'account/requestPasswordReset', { params });
  }

  // tslint:disable-next-line: typedef
  public checkEmailExists(email: string) {
    return this.http.get(this.baseUrl + 'account/emailExists?email=' + email);
  }

  // tslint:disable-next-line: typedef
  public checkUserExists(username: string) {
    return this.http.get(this.baseUrl + 'account/userExists?username=' + username);
  }

  public register(values: any): Observable<boolean> {
    return this.http.post<boolean>(this.baseUrl + 'account/register', values);
    // .pipe(map((user: IUser) => {
    //   if (user) {
    //     localStorage.setItem('token', user.token);
    //     this.currentUserSource.next(user);
    //   }
    // })
  }

  // tslint:disable-next-line: typedef
  public loadCurrentUser(token: string) {
    if (token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.get(this.baseUrl + 'account ', { headers }).pipe(
      map((user: IUser) => {
        if (user) {
          this.currentUserSource.next(user);
          localStorage.setItem('token', user.token);
        }
      })
    );
  }

  public checkRoles(allowedRoles: string[], user: IUser): boolean {
    if (!user)
    {
      return false;
    }

    const userRoles = jwtDecode<any>(user.token).role as Array<string>;
    if (!userRoles)
    {
      return false;
    }

    let match = false;

    allowedRoles.forEach(element => {
      if (userRoles.includes(element)) {
        match = true;
      }
    });

    return match;
  }

  public getRolesFromToken(token: string): Array<string> {
    return jwtDecode<any>(token).role as Array<string>;
  }

  // tslint:disable-next-line: typedef
  public getUserAddress() {
    return this.http.get<IAddress>(this.baseUrl + 'account/address');
  }

  public getUserInfos() {
    return this.http.get<IPersonalInformation>(this.baseUrl + 'account/info');
  }

  // tslint:disable-next-line: typedef
  public updateUserAddress(address: IAddress) {
    return this.http.put<IAddress>(this.baseUrl + 'account/address', address);
  }

  public updateUserInformations(personalInfo: IPersonalInformation) {
    return this.http.put<IPersonalInformation>(this.baseUrl + 'account/info', personalInfo);
  }

  // tslint:disable-next-line: typedef
  public updateUser(values: any) {
    return this.http.put<IUser>(this.baseUrl + 'account/updateProfile', values).pipe(map((user: IUser) => {
      if (user) {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
        return user;
      }
    }));
  }

  // tslint:disable-next-line: typedef
  public updateUserPassword(values: any) {
    return this.http.put(this.baseUrl + 'account/updatePassword', values);
  }
}
