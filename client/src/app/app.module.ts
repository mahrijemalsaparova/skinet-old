import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from './core/core.module';
import { HomeModule } from './home/home.module';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    // ngx-bootstrap indirildikten sonra otomatik eklenir.
    BrowserAnimationsModule,
    // API 'den (back-end tarafından) verilerimizi çagırabilmemiz için import etmemiz gereken module.
    HttpClientModule,
    // içindeki NavBarComponente bütün moduller ulaşabilir.
    CoreModule,
   // ShopModule, gerek kalmadı çünkü kendi içinde routing yani lazyloading edilecek
    HomeModule
  ],
  providers: [
    // error.interceptor.ts kullanabilmek için
    // muiti true olması demek bizim kendi interceptorumuz çalışmasın, app.modulun kendi interceptorları ile beraber çalışsın demek.
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
