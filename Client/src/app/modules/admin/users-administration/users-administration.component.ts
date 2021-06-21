import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IPagination } from 'src/app/shared/models/IPagination';
import { IUserForAdministration } from 'src/app/shared/models/IUser';
import { UsersParams } from 'src/app/shared/models/Params';
import { AdminService } from '../admin.service';
import { UserDeleteModalComponent } from './user-delete-modal/user-delete-modal.component';
import { UserUpdateModalComponent } from './user-update-modal/user-update-modal.component';

@Component({
  selector: 'app-users-administration',
  templateUrl: './users-administration.component.html',
  styleUrls: ['./users-administration.component.scss']
})
export class UsersAdministrationComponent implements OnInit {
  pagination: IPagination<IUserForAdministration>;
  roles: string[] = [];
  params = new UsersParams();

  constructor(private adminService: AdminService, private modalService: NgbModal) { }

  ngOnInit(): void {
    this.params.pageSize = 10;
    this.params.pageIndex = 1;
    this.getUsers();
    this.getRoles();
  }

  getUsers(): void {
    this.adminService.getUsers(this.params).subscribe(
      res => {
        this.pagination = res;
      },
      err => {
        console.error(err);
      }
    );
  }

  getRoles(): void {
    this.adminService.getRoles().subscribe(
      res => {
        this.roles = res;
      },
      err => {
        console.error(err);
      }
    );
  }

  updateUserModal(user: IUserForAdministration, roles: string[]): void {
    const initialState = {
      user,
      roles
    };

    const modalRef = this.modalService.open(UserUpdateModalComponent, {
      size: 'md',
    });

    modalRef.componentInstance.initialState = initialState;
    modalRef.componentInstance.parent = this;
  }

  deleteUserModal(user: IUserForAdministration): void {
    const initialState = {
      user,
    };

    const modalRef = this.modalService.open(UserDeleteModalComponent, {
      size: 'md',
    });

    modalRef.componentInstance.initialState = initialState;
    modalRef.componentInstance.parent = this;
  }

  public OnPageChanged(event: any): void {
    if (this.params.pageIndex !== event) {
      this.params.pageIndex = event;
      this.getUsers();
    }
  }
}
