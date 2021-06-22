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
  @ViewChild('minPrice', { static: false }) minPrice: ElementRef;
  @ViewChild('maxPrice', { static: false }) maxPrice: ElementRef;
  @ViewChild('inStock', { static: false }) inStock: ElementRef;

  products: IProduct[];
  categories: IProductCategory[];
  types: IProductType[];
  totalCount: number;
  pageSize = 15;
  shopParams = new ShopParams();

  isSortSearchCollapsed = false;
  isPricingCollapsed = true;
  isCategoriesCollapsed = true;
  isTypesCollapsed = true;

  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to Heigh', value: 'priceAsc' },
    { name: 'Price: Heigh to Low', value: 'priceDesc' }
  ];

  pageSizes = [6, 15, 25, 50];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();

    this.getCategories();

    this.getTypes();
  }

  public getProducts(): void {
    this.shopParams.pageSize = this.pageSize;
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
      this.categories = [{ id: 0, name: 'All' }, ...response];
    }, error => {
      console.error(error);
    });
  }

  private getTypes(): void {
    this.shopService.getTypes().subscribe(response => {
      this.types = [{ id: 0, name: 'All', productCategory: 'All' }, ...response];
    }, error => {
      console.error(error);
    });
  }

  public OnCategorySelected(categoryId: number): void {
    this.shopParams.categoryId = categoryId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
    this.shopService.getTypesOfCategory(categoryId).subscribe(
      (res) => this.types = [{ id: 0, name: 'All', productCategory: 'All' }, ...res],
      (err) => console.log(err)
    )
  }

  public OnTypeSelected(typeId: number): void {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  public OnSortSelected(sort: string): void {
    this.shopParams.sort = sort;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  public OnPageSize(pageSize: number): void {
    this.pageSize = pageSize;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  public OnPageChanged(event: any): void {
    if (this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getProducts();
    }
  }

  public OnSearch(): void {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  public OnMinPrice(): void {
    this.shopParams.minPrice = this.minPrice.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  public OnMaxPrice(): void {
    this.shopParams.maxPrice = this.maxPrice.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  public OnInStock(): void {
    this.shopParams.inStock = this.inStock.nativeElement.checked;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  public onReset(): void {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
