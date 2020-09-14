import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ProductDetailsComponent } from './shop/product-details/product-details.component';
import { ShopComponent } from './shop/shop.component';

const routes: Routes = [
  // adresin sonunda boşluk olursa HomeComponent sayfasına gidilir.
  {path: '', component: HomeComponent},
  // burada lazyloading yaptığımız için {path: 'shop', component: ShopComponent} yerine bu şekilde yazıyoruz.
  // çünkü shop route işlemimiz shop.module.ts'in kendi içinde gerçekleşecek app.module.ts'de değil.
  {path: 'shop', loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule)},
  {path: 'shop/:id', component: ProductDetailsComponent},
  // yalnış veya hatalı addres için home kısmına redirect eder
  {path: '**', redirectTo: '', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
