import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { HomeComponent } from './home/home.component';
import { ProductDetailsComponent } from './shop/product-details/product-details.component';

const routes: Routes = [
  // adresin sonunda boşluk olursa HomeComponent sayfasına gidilir.
  {path: '', component: HomeComponent},
  {path: 'test-error', component: TestErrorComponent},
  {path: 'server-error', component: ServerErrorComponent},
  {path: 'not-found', component: NotFoundComponent},
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
