import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { IUserForAdministration } from 'src/app/shared/models/IUser';
import { AdminService } from '../../admin.service';
import { UsersAdministrationComponent } from '../users-administration.component';

@Component({
  selector: 'app-user-delete-modal',
  templateUrl: './user-delete-modal.component.html',
  styleUrls: ['./user-delete-modal.component.scss']
})
export class UserDeleteModalComponent implements OnInit {
  user: IUserForAdministration;
  errors = [];
  loading = false;
  initialState: any;
  parent: UsersAdministrationComponent;

  constructor(public activeModal: NgbActiveModal, private administrationService: AdminService, private toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.user = this.initialState.user;
  }

  deleteUser(): void {
    this.loading = true;
    this.administrationService.deleteUser(this.user.id).subscribe(
      () => {
        this.toastr.success(`The user ${this.user.userName} has been deleted correctly`);
        this.activeModal.close();
        this.loading = false;
        this.parent.getUsers();
      },
      err => {
        this.errors = err.errors;
        this.loading = false;
      }
    );
  }
}
