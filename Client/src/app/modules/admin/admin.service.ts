import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IPagination } from 'src/app/shared/models/IPagination';
import { IUserForAdministration, UserForUpdate } from 'src/app/shared/models/IUser';
import { UsersParams } from 'src/app/shared/models/Params';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  public getUsers(usersParams: UsersParams): Observable<IPagination<IUserForAdministration>> {
    let params = new HttpParams();
    params = params.append('pageIndex', usersParams.pageIndex.toString());
    params = params.append('pageSize', usersParams.pageSize.toString());

    return this.http.get<IPagination<IUserForAdministration>>(this.baseUrl + 'administration/users',
      { observe: 'response', params })
      .pipe(
        map(response => {
          return response.body;
        }));
  }

  public getRoles(): Observable<string[]> {
    return this.http.get<string[]>(this.baseUrl + 'administration/roles');
  }

  public updateRoles(values: UserForUpdate): Observable<any> {
    return this.http.put<UserForUpdate>(this.baseUrl + 'administration/updateRoles', values);
  }

  public deleteUser(userId: string): Observable<any> {
    return this.http.delete(this.baseUrl + 'administration/deleteUser', { params: { userId } });
  }
}
