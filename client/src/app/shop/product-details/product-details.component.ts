import { IBasketItem } from './../../shared/models/basket';
import { BasketService } from 'src/app/basket/basket.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
  quantity = 1;
                                                                // burada ActivatedRoute getProduct metodu için gerekli parametreyi taşır.
                                                                // her bir ürün için kendi bilgisini döndürür sayfada
  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute, private bcService: BreadcrumbService,
              private basketService: BasketService) {
        this.bcService.set('@productDetails', ''); // sayfayı loading yaparken product seçtiğimiz zaman var olan ismi gözükmesin
                // yani her seferinde taze yüklensin ki temiz olsun arkası
               }


  ngOnInit() {
    this.loadProduct();
  }

  // detail kısmındaki add to cart için
  addItemToBasket() {
   this.basketService.addItemToBasket(this.product, this.quantity);
  }
  // detail kısmındaki artı için
  incrementQuantity() {
      this.quantity++;
  }
   // detail kısmındaki eksi için
  decrementQuantity() {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }


  loadProduct()  {// burada + işareti number yapar değeri
    this.shopService.getProduct(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(productResponse => {
      this.product = productResponse;
      // shop-routing.module.ts teki data: {breadcrumb: {alias: 'productDetails'}} için
      this.bcService.set('@productDetails', productResponse.name);
    }, error => {
      console.log(error);
    });
  }

}
