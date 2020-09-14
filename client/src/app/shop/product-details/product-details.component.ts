import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;
                                                                // burada ActivatedRoute getProduct metodu için gerekli parametreyi taşır.
                                                                // her bir ürün için kendi bilgisini döndürür sayfada
  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.loadProduct();
  }

  loadProduct()  {// burada + işareti number yapar değeri
    this.shopService.getProduct(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(productResponse => {
      this.product = productResponse;
    }, error => {
      console.log(error);
    });
  }

}
