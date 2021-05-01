import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { HomeComponent } from './modules/home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  {path: 'home', component: HomeComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: 'not-found', component: NotFoundComponent},
  { path: 'shop', loadChildren: () => import('./modules/shop/shop.module').then(mod => mod.ShopModule) },
  { path: 'cart', loadChildren: () => import('./modules/cart/cart.module').then(mod => mod.CartModule) },
  {path: '**', redirectTo: 'not-found', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
