import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProduct } from 'src/app/shared/models/IProduct';
import { IProductCategory } from 'src/app/shared/models/IProductCategory';
import { IProductType } from 'src/app/shared/models/IProductType';
import { ShopParams } from 'src/app/shared/models/Params';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search', { static: false }) searchTerm: ElementRef;
  products: IProduct[];
  categories: IProductCategory[];
  types: IProductType[];
  totalCount: number;
  shopParams = new ShopParams();

  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to Heigh', value: 'priceAsc'},
    {name: 'Price: Heigh to Low', value: 'priceDesc'}
  ];

  
  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();

    this.getCategories();

    this.getTypes();
  }

  private getProducts(): void {
    this.shopParams.pageSize = 9;
    this.shopService.getProducts(this.shopParams).subscribe(response => {
      this.products = response.data;
      this.shopParams.pageNumber = response.pageIndex;
      this.shopParams.pageSize = response.pageSize;
      this.totalCount = response.count;
    }, error => {
      console.error(error);
    });
  }

  private getCategories(): void {
    this.shopService.getCategories().subscribe(response => {
      this.categories = [{id: 0, name: 'All'}, ...response];
    }, error => {
      console.error(error);
    });
  }

  private getTypes(): void {
    this.shopService.getTypes().subscribe(response => {
      this.types = [{id: 0, name: 'All', productCategory: 'All'}, ...response];
    }, error => {
      console.error(error);
    });
  }

public OnCategorySelected(categoryId: number): void {
    this.shopParams.categoryId = categoryId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  public OnTypeSelected(typeId: number): void {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  public OnSortSelected(sort: string): void {
    this.shopParams.sort = sort;
    this.getProducts();
  }

  public OnPageChanged(event: any): void {
    if (this.shopParams.pageNumber !== event)
    {
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
  }

  public OnSearch(): void
  {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  public onReset(): void
  {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
