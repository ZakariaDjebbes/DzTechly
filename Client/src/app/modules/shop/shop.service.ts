import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IAdditionalInfoName } from 'src/app/shared/models/IAdditionalInfoName';
import { IPagination } from 'src/app/shared/models/IPagination';
import { IProduct } from 'src/app/shared/models/IProduct';
import { IProductCategory } from 'src/app/shared/models/IProductCategory';
import { IProductToCreate } from 'src/app/shared/models/IProductToCreate';
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
      params = params.append('categoryId', shopParams.categoryId.toString());
    }

    if (shopParams.typeId !== 0) {
      params = params.append('typeId', shopParams.typeId.toString());
    }

    if (shopParams.minPrice !== 0) {
      params = params.append('minPrice', shopParams.minPrice.toString());
    }

    if (shopParams.maxPrice !== 0) {
      params = params.append('maxPrice', shopParams.maxPrice.toString());
    }

    if (shopParams.search) {
      params = params.append('search', shopParams.search);
    }

    params = params.append('inStock', shopParams.inStock.toString());
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

  public getTypesOfCategory(categoryId: number): Observable<IProductType[]> {
    let params = new HttpParams();
    params = params.append('categoryId', categoryId.toString());
    return this.http.get<IProductType[]>(this.baseUrl + 'products/typesOfCategory', { observe: 'response', params })
      .pipe(
        map(response => {
          return response.body;
        })
      );;
  }

  public getAdditionalInfoNamesOfType(typeId: number): Observable<IAdditionalInfoName[]> {
    let params = new HttpParams();
    params = params.append('typeId', typeId.toString());
    return this.http.get<IAdditionalInfoName[]>(this.baseUrl + 'products/additionalInfoNames', { observe: 'response', params })
      .pipe(
        map(response => {
          return response.body;
        })
      );;
  }

  public getCategories(): Observable<IProductCategory[]> {
    return this.http.get<IProductCategory[]>(this.baseUrl + 'products/categories');
  }

  public getTypes(): Observable<IProductType[]> {
    return this.http.get<IProductType[]>(this.baseUrl + 'products/types');
  }

  public getProduct(id: number) {
    return this.http.get<IProduct>(this.baseUrl + 'products/' + id);
  }

  public getReviewsOfProduct(id: number, reviewParams: ReviewParams): Observable<IPagination<IReview>> {
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

  public reviewProduct(review: IReviewToCreate) {
    return this.http.post(this.baseUrl + 'products/review', review);
  }

  public deleteReview(id: number) {
    return this.http.delete(this.baseUrl + 'products/review?id=' + id);
  }

  public addProduct(product: IProductToCreate) {
    return this.http.post(this.baseUrl + 'products', product);
  }

  public updateProduct(product: IProduct) {
    return this.http.put(this.baseUrl + 'products', product);
  }

  public deleteProduct(id: number) {
    return this.http.delete(this.baseUrl + 'products?productId=' + id);
  }

  public addToWaitingList(id: number) {
    return this.http.post(this.baseUrl + 'products/waitingList?productId=' + id, null);
  }
}
