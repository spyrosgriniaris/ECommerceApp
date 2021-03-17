import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { AuthGuard } from './core/guards/auth.guard';


const routes: Routes = [
  {path: '', component: HomeComponent, data: {breadcrump: 'Home'}},
  {path: 'test-error', component: TestErrorComponent, data: {breadcrump: 'Test Errors'}},
  {path: 'server-error', component: ServerErrorComponent, data: {breadcrump: 'Server Error'}},
  {path: 'not-found', component: NotFoundComponent, data: {breadcrump: 'Not Found'}},
  // shop module will be activated when we access the shop path and not from the beggining
  {path: 'shop', loadChildren: () => import('././shop/shop.module').then(mod => mod.ShopModule), data: {breadcrump: 'Shop'}},
  // {path: 'shop/:id', component: ProductDetailsComponent},
  {path: 'basket', loadChildren: () => import('././basket/basket.module').then(mod => mod.BasketModule), data: {breadcrump: 'Basket'}},
  {path: 'checkout', canActivate: [AuthGuard], loadChildren: () => import('././checkout/checkout.module').then(mod => mod.CheckoutModule), data: {breadcrump: 'Checkout'}},
  {path: 'account', loadChildren: () => import('././account/account.module').then(mod => mod.AccountModule), data: {breadcrump: {skip: true}}},
  {path: '**', redirectTo: 'not-found', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
