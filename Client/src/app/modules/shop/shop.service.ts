import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IPagination } from 'src/app/shared/models/IPagination';
import { IProduct } from 'src/app/shared/models/IProduct';
import { IProductCategory } from 'src/app/shared/models/IProductCategory';
import { IProductType } from 'src/app/shared/models/IProductType';
import { IReview } from 'src/app/shared/models/IReview';
import { IReviewToCreate } from 'src/app/shared/models/IReviewToCreate';
import { ReviewParams, ShopParams } from 'src/app/shared/models/Params';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  public getProducts(shopParams: ShopParams): Observable<IPagination<IProduct>> {
    let params = new HttpParams();

    if (shopParams.categoryId !== 0) {
      params = params.append('brandId', shopParams.categoryId.toString());
    }

    if (shopParams.typeId !== 0) {
      params = params.append('typeId', shopParams.typeId.toString());
    }

    if (shopParams.search)
    {
      params = params.append('search', shopParams.search);
    }

    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());

    return this.http.get<IPagination<IProduct>>(this.baseUrl + 'products', { observe: 'response', params })
      .pipe(
        map(response => {
          return response.body;
        })
      );
  }

  public getCategories(): Observable<IProductCategory[]> {
    return this.http.get<IProductCategory[]>(this.baseUrl + 'products/categories');
  }

  public getTypes(): Observable<IProductType[]> {
    return this.http.get<IProductType[]>(this.baseUrl + 'products/types');
  }

  // tslint:disable-next-line: typedef
  public getProduct(id: number){
    return this.http.get<IProduct>(this.baseUrl + 'products/' + id);
  }

  public getReviewsOfProduct(id: number, reviewParams: ReviewParams): Observable<IPagination<IReview>>{
    let params = new HttpParams();
    params = params.append('pageIndex', reviewParams.pageNumber.toString());
    params = params.append('pageSize', reviewParams.pageSize.toString());
    return this.http.get<IPagination<IReview>>(this.baseUrl + 'products/reviews/' + id, { observe: 'response', params })
    .pipe(
      map(response => {
        return response.body;
      })
    );
  }

  // tslint:disable-next-line: typedef
  public reviewProduct(review: IReviewToCreate){
    return this.http.post(this.baseUrl + 'products/review', review);
  }
}
