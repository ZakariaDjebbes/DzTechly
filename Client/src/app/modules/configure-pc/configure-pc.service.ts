import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IProduct } from 'src/app/shared/models/IProduct';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ConfigurePcService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  public getProductsOfType(typeId: number): Observable<IProduct[]> {
    return this.http.get<IProduct[]>(this.baseUrl + "configure/"+ typeId);
  }

  public getCpus(id: number): Observable<IProduct[]> {
    return this.http.get<IProduct[]>(this.baseUrl + "configure/CPU/"+ id);
  }

  public getRams(id: number): Observable<IProduct[]> {
    return this.http.get<IProduct[]>(this.baseUrl + "configure/RAM/"+ id);
  }

  public getGpus(id: number): Observable<IProduct[]> {
    return this.http.get<IProduct[]>(this.baseUrl + "configure/GPU/"+ id);
  }
}
