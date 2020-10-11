import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule} from 'ngx-bootstrap/pagination';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { PagerComponent } from './components/pager/pager.component';
import { OrderTotalsComponent } from './components/order-totals/order-totals.component';
import { ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TextInputComponent } from './components/text-input/text-input.component';



@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent,
    OrderTotalsComponent,
    TextInputComponent
  ],
  imports: [
    CommonModule,
    // forRoot is used when a module is “eager,” that is, it is not lazy-loaded (loads when the application starts).
    PaginationModule.forRoot(),
    // home sayfası için
   CarouselModule.forRoot(),
   // login formu inputları için
   ReactiveFormsModule,
   // dropdown menüsü için
   BsDropdownModule.forRoot(),

  ],
  exports: [
    PaginationModule,
    PagingHeaderComponent,
    PagerComponent,
    CarouselModule,
    OrderTotalsComponent,
    ReactiveFormsModule,
     // dropdown menüsü için
    BsDropdownModule,
    // login html sayfasının kalabalığını azaltmak için oluşturduğumuz component yani reusable text input
    TextInputComponent
  ]
})
export class SharedModule { }
