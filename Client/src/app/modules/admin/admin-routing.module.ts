import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { RouterModule, Routes } from '@angular/router';
import { OrderHistoryDetailsComponent } from './orders-history/order-history-details/order-history-details.component';


const routes: Routes = [
  { path: '', component: AdminComponent},
  { path: ':id', component: OrderHistoryDetailsComponent},

]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AdminRoutingModule { }
