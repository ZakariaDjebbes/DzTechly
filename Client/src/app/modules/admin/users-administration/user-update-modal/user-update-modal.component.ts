import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { IUserForAdministration, UserForUpdate } from 'src/app/shared/models/IUser';
import { AdminService } from '../../admin.service';
import { UsersAdministrationComponent } from '../users-administration.component';

@Component({
  selector: 'app-user-update-modal',
  templateUrl: './user-update-modal.component.html',
  styleUrls: ['./user-update-modal.component.scss']
})
export class UserUpdateModalComponent implements OnInit {
  updateUserForm: FormGroup;
  user: IUserForAdministration;
  roles: string[];
  loading = false;
  errors: any[] = null;
  confirmationLoading = false;
  passwordResetLoading = false;
  initialState: any;
  parent: UsersAdministrationComponent;
  data = [];

  constructor(public activeModal: NgbActiveModal, private formBuilder: FormBuilder,
    private adminService: AdminService, private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.createForm();
    this.user = this.initialState.user;
    this.roles = this.initialState.roles;

    for (const role of this.roles) {
      if (this.user.userRoles.includes(role))
        this.data.push(
          {
            role,
            selected: true
          }
        )
      else
        this.data.push({
          role,
          selected: false
        })
    }

    console.log(this.data);
  }

  updateUserRoles(): void {
    let data = new UserForUpdate();
    data.id = this.user.id;
    data.roles = this.data.filter(x => x.selected).map(x => x.role);
    

    this.adminService.updateRoles(data).subscribe(() => {
      this.toastr.success(`The user ${this.user.userName} has been updated correctly`);
      this.activeModal.close();
      this.loading = false;
      this.parent.getUsers();
    },
      err => {
        console.log(err);
        this.errors = err.errors;
        this.loading = false;
      });
  }

  createForm(): void {
    this.updateUserForm = this.formBuilder.group(
      {
        roles: [null, [Validators.required]]
      }
    );
  }

  onChecked(role: string): void {
    const checked = document.getElementById(role)["checked"];
    this.data.filter(x => x.role === role)[0].selected = checked;
  }
}
