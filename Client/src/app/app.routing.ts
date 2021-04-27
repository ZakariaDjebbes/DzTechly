import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './modules/home/home.component';

const routes: Routes =[
    { path: '', component: HomeComponent },
    { path: 'shop', loadChildren: () => import('./modules/shop/shop.module').then(mod => mod.ShopModule)},
    { path: '**', redirectTo: ''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
