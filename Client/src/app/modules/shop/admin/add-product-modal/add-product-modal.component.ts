import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IAdditionalInfoName, IAdditionalInfoValue } from 'src/app/shared/models/IAdditionalInfoName';
import { IProductCategory } from 'src/app/shared/models/IProductCategory';
import { IProductType } from 'src/app/shared/models/IProductType';
import * as _ from 'underscore';
import { ShopService } from '../../shop.service';
import { AddCategoryModalComponent } from '../add-category-modal/add-category-modal.component';
import { AddInfoModalComponent } from '../add-info-modal/add-info-modal.component';
import { IProductToCreate } from 'src/app/shared/models/IProductToCreate';
import { IProduct } from 'src/app/shared/models/IProduct';
import { ToastrService } from 'ngx-toastr';
import { ShopComponent } from '../../shop.component';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-add-product-modal',
  templateUrl: './add-product-modal.component.html',
  styleUrls: ['./add-product-modal.component.scss'],
  host: {
    '[style.display]': '"flex"',
    '[style.flex-direction]': '"column"',
    '[style.overflow]': '"auto"'
  }
})
export class AddProductModalComponent implements OnInit {
  shopComponent: ShopComponent;
  baseImageUrl = environment.imagesUrl;
  maxDescriptionLength = 300;
  addProductForm: FormGroup;
  remainingDescriptionLength = this.maxDescriptionLength;
  url = "./../../../../../assets/img/brand/placeholder.png";
  types: IProductType[];
  categories: IProductCategory[];
  additionalInfoNames: IAdditionalInfoName[][];
  isCollapsed: Boolean[];
  loading = false;
  errors = null; 
  currentImageName: string;

  constructor(public activeModal: NgbActiveModal, private shopService: ShopService, private modalService: NgbModal, private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.createAddProductForm();
    this.getCategories();
  }

  createAddProductForm(): void {
    this.addProductForm = new FormGroup({
      name: new FormControl('', [Validators.required]),
      description: new FormControl(''),
      price: new FormControl('0', [Validators.required, Validators.min(0)]),
      picture: new FormControl('', [Validators.required]),
      quantity: new FormControl('0', [Validators.required, Validators.min(0)]),
      additionalInformations: new FormControl('', []),
      productTypeId: new FormControl('', [Validators.required]),
      productCategoryId: new FormControl('', [Validators.required])
    });
  }

  onSubmit(): void {
    this.addProductForm.get('additionalInformations').setValue(this.getTechnicalSheet());
    const productToAdd: IProductToCreate = this.addProductForm.value;
    productToAdd.pictureUrl = this.baseImageUrl + this.currentImageName;
    this.loading = true;
    this.shopService.addProduct(productToAdd).subscribe(
      (res: IProduct) => {
        this.toastr.success(`Product ${res.name} has been correctly added.`);
        this.shopComponent.getProducts();
        this.activeModal.close();
      },
      (error) => {
        this.errors = error.errors;
        this.loading = false;
        this.toastr.error(error.message);
      },
      () => {
        this.loading = false;
      }
    );
  }

  getRemainingCommentLength(): void {
    const currentLength = this.addProductForm.get('description').value.length;
    this.remainingDescriptionLength = this.maxDescriptionLength - currentLength;
  }

  onImageChanged(event): void {
    if (event.target.files) {
      let reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]);
      reader.onload = (e: any) => {
        this.url = e.target.result;
      }

      const files: FileList = event.target.files;
      this.currentImageName = files.item(0).name;
    }
  }

  private getCategories(): void {
    this.shopService.getCategories().subscribe(response => {
      this.categories = response;
      this.getTypes(this.categories[0].id);
    }, error => {
      console.error(error);
    });
  }

  getTypes(categoryId: number): void {
    this.shopService.getTypesOfCategory(categoryId).subscribe(response => {
      this.types = response;
      this.getAdditionalInfoNames(this.types[0].id);
    }, error => {
      console.error(error);
    });
  }

  getAdditionalInfoNames(typeId: number): void {
    this.shopService.getAdditionalInfoNamesOfType(typeId).subscribe(res => {
      this.additionalInfoNames = _.toArray(_.groupBy(res, 'additionalInfoCategoryId'));
      this.isCollapsed = new Array(this.additionalInfoNames.length).fill(false);
      this.isCollapsed[0] = true;
    }, err => {
      console.error(err);
    });
  }

  private getTechnicalSheet(): IAdditionalInfoValue[] {
    let res: IAdditionalInfoValue[] = [];
    for (const item of this.additionalInfoNames) {
      for (const info of item) {
        let value: IAdditionalInfoValue = {
          unit: info.unit,
          value: document.getElementById(info.name)["value"],
          categoryId: info.additionalInfoCategoryId,
          category: info.additionalInfoCategoryName,
          name: info.name,
          nameId: info.additionalInfoNameId
        }
        res.push(value)
      }
    }

    return res;
  }

  collapse(i: number): void {
    this.isCollapsed[i] = !this.isCollapsed[i];

    for (let x = 0; x < this.isCollapsed.length; x++) {
      if (x !== i) this.isCollapsed[x] = false;
    }
  }

  addAdditionalCategory(category: string, firstInfo: string, unit: string): void {
    let additionalInfosToAdd: IAdditionalInfoName[] = new Array();
    let info: IAdditionalInfoName;
    let currentTypeId = this.addProductForm.get('productTypeId').value;
    info = {
      name: firstInfo,
      additionalInfoCategoryName: category,
      productTypeId: currentTypeId,
      unit
    }
    additionalInfosToAdd.push(info)
    this.additionalInfoNames.push(additionalInfosToAdd)
  }

  addAddtionalInfo(index: number, name: string, unit: string): void {
    let additionalInfoName: IAdditionalInfoName;
    additionalInfoName = {
      name,
      productTypeId: this.additionalInfoNames[index][0].productTypeId,
      additionalInfoCategoryName: this.additionalInfoNames[index][0].additionalInfoCategoryName,
      unit
    }

    this.additionalInfoNames[index].push(additionalInfoName);
  }

  openAddCategoryModal(): void {
    const modalRef = this.modalService.open(AddCategoryModalComponent, {
      size: 'sm',
    });
    modalRef.result.then(res => {
      this.addAdditionalCategory(res[0], res[1], res[2]);
    }, (reason) => {
    });
  }

  openAddInfoModal(index: number): void {
    const modalRef = this.modalService.open(AddInfoModalComponent, {
      size: 'sm',
    });
    modalRef.result.then(res => {
      this.addAddtionalInfo(index, res[0], res[1]);
    }, (reason) => {
    })
  }
}
