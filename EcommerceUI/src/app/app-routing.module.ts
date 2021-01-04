import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ShopComponent } from './shop/shop.component';
import { ProductDetailsComponent } from './shop/product-details/product-details.component';


const routes: Routes = [
  {path: '', component: HomeComponent},
  // shop module will be activated when we access the shop path and not from the beggining
  {path: 'shop', loadChildren: () => import('././shop/shop.module').then(mod => mod.ShopModule)},
  // {path: 'shop/:id', component: ProductDetailsComponent},
  {path: '**', redirectTo: '', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
