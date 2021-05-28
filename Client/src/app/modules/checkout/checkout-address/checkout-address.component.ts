import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { IAddress } from 'src/app/shared/models/IAddress';
import { IPersonalInformation } from 'src/app/shared/models/IPersonalInformation';
import { AccountService } from '../../account/account.service';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss']
})
export class CheckoutAddressComponent implements OnInit {
  @Input() checkoutForm: FormGroup;
  addressLoading = false;
  personalInfoLoading = false;
  
  constructor(private accountService: AccountService, private toastr: ToastrService) {
  }

  ngOnInit(): void {
  }

  saveUserAddress(): void {
    this.addressLoading = true;
    this.accountService.updateUserAddress(this.checkoutForm.get('addressForm').value)
      .subscribe((address: IAddress) => {
        this.toastr.success('Address saved');
        this.checkoutForm.get('addressForm').reset(address);
        this.addressLoading = false;
      }, error => {
        this.toastr.error(error.message);
        console.log(error);
        this.addressLoading = false;
      });
  }

  saveUserInformations(): void {
    this.personalInfoLoading = true;
    this.accountService.updateUserInformations(this.checkoutForm.get('personalInformationForm').value)
      .subscribe((info: IPersonalInformation) => {
        this.toastr.success('Personal informations saved');
        this.checkoutForm.get('personalInformationForm').reset(info);
        this.personalInfoLoading = false;
      }, error => {
        this.toastr.error(error.message);
        console.log(error);
        this.personalInfoLoading = false;
      });
  }
}
