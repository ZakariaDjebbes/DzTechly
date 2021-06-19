import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { IProduct, IProductInfo } from 'src/app/shared/models/IProduct';
import { environment } from 'src/environments/environment';
import { ItemDetailsComponent } from '../../item-details/item-details.component';
import { ShopComponent } from '../../shop.component';
import { ShopService } from '../../shop.service';

@Component({
  selector: 'app-update-product-modal',
  templateUrl: './update-product-modal.component.html',
  styleUrls: ['./update-product-modal.component.scss']
})
export class UpdateProductModalComponent implements OnInit {
  itemDetails: ItemDetailsComponent;
  baseImageUrl = environment.imagesUrl;
  baseApiUrl = environment.apiUrl;
  shopComponent: ShopComponent;
  product: IProduct;
  loading = false;
  errors: any[] = null;
  currentPicture: File;
  maxDescriptionLength = 300;
  remainingDescriptionLength = this.maxDescriptionLength;
  url: string;
  updateProductForm: FormGroup;
  infoCategories: Array<[string, IProductInfo]>;
  isCollapsed: Boolean[];
  currentImageName: string;

  constructor(public activeModal: NgbActiveModal, private shopService: ShopService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.url = this.product.pictureUrl;
    const split = this.product.pictureUrl.split('/');
    this.currentImageName = this.baseImageUrl + split[split.length - 1];
    console.log(this.currentImageName)
    this.createUpdateProductForm();
    this.infoCategories = Object.entries(this.product.technicalSheet.productAddtionalInfos);
    this.isCollapsed = new Array(this.infoCategories.length).fill(false);
    this.isCollapsed[0] = true;
  }

  public onSubmit(): void {
    this.loading = true;
    this.product.name = this.updateProductForm.get('name').value;
    this.product.description = this.updateProductForm.get('description').value;
    this.product.price = this.updateProductForm.get('price').value;
    this.product.quantity = this.updateProductForm.get('quantity').value;
    this.product.pictureUrl = this.currentImageName;

    console.log(this.product);

    this.shopService.updateProduct(this.product).subscribe(
      (res: IProduct) => {
        this.toastr.success(`Product #${res.id} - ${res.name} has been correctly updated.`);
        if (this.shopComponent)
          this.shopComponent.getProducts();
        if(this.itemDetails)
          this.itemDetails.loadPrudct();
        this.activeModal.close(true);
        this.loading = false;
      },
      (error) => {
        this.toastr.error(error.meesage);
        this.loading = false;
      }
    );
  }

  collapse(i: number): void {
    this.isCollapsed[i] = !this.isCollapsed[i];

    for (let x = 0; x < this.isCollapsed.length; x++) {
      if (x !== i) this.isCollapsed[x] = false;
    }
  }

  onChange(i: number, j: number): void {
    let key = this.infoCategories[i][0];
    let name = this.infoCategories[i][1][j].additionalInfoName;
    let val = document.getElementById(name)["value"];
    this.product.technicalSheet.productAddtionalInfos[key][j].additionalInfoValue = val;
    console.log(this.product.technicalSheet);
  }

  createUpdateProductForm(): void {
    this.updateProductForm = new FormGroup({
      name: new FormControl(this.product.name, [Validators.required]),
      description: new FormControl(this.product.description),
      price: new FormControl(this.product.price, [Validators.required, Validators.min(0)]),
      picture: new FormControl(this.product.pictureUrl, [Validators.required]),
      quantity: new FormControl(this.product.quantity, [Validators.required, Validators.min(0)]),
    });
  }

  onImageChanged(event): void {
    if (event.target.files) {
      let reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]);
      reader.onload = (e: any) => {
        this.url = e.target.result;
      }

      const files: FileList = event.target.files;
      this.currentPicture = files.item(0);
      this.currentImageName = this.baseImageUrl + files.item(0).name;
    }
  }

  getRemainingCommentLength(): void {
    const currentLength = this.updateProductForm.get('description').value.length;
    this.remainingDescriptionLength = this.maxDescriptionLength - currentLength;
  }
}
