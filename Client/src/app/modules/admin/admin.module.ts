import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { AdminRoutingModule } from './admin-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { UsersAdministrationComponent } from './users-administration/users-administration.component';
import { UserDeleteModalComponent } from './users-administration/user-delete-modal/user-delete-modal.component';
import { UserUpdateModalComponent } from './users-administration/user-update-modal/user-update-modal.component';
import { OrderHistoryDetailsComponent } from './orders-history/order-history-details/order-history-details.component';
import { OrdersHistoryComponent } from './orders-history/orders-history.component';

@NgModule({
  declarations: [AdminComponent, UsersAdministrationComponent, UserDeleteModalComponent, UserUpdateModalComponent, OrderHistoryDetailsComponent, OrdersHistoryComponent],
  imports: [
    CommonModule,
    AdminRoutingModule,
    SharedModule
  ]
})
export class AdminModule { }
