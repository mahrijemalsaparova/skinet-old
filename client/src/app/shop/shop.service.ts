import { HttpClient, HttpParams} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IPagination } from '../shared/models/pagination';
import { IType } from '../shared/models/productType';
import {map} from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';
import { IProduct } from '../shared/models/product';
// uygulama çalıştığı sürece servicler çalışır singletondur. yani illa çagırmamıza gerek yok arka planda hep çalışır.
// bu özelliği ise verileri çekmekte bolca işimize yarar. yani biz verileri istediğimiz zaman kullanmak için bekletebiliriz
// burada hazır vaziyette.
@Injectable({
  providedIn: 'root' // uygulama çalıştığı zaman çalışır ve rootun anlamı app.module.ts tarafından çalışması için desteklendi demektir.
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';



  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams) {

    let params = new HttpParams();

    if (shopParams.brandId !== 0) {
      params = params.append('brandId', shopParams.brandId.toString());
    }

    if (shopParams.typeId !== 0) {
      params = params.append('typeId', shopParams.typeId.toString());
    }

    if (shopParams.search) {
      params = params.append('search', shopParams.search);
    }


    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageSize', shopParams.pageSize.toString());

                                                                // burada observe ettiğimiz için geriye http response döner body dönmez
                                                  // gelen bu http body responsu IPagination'a dönüştürmek için pipe metodunu kullanıyoruz.
    return this.http.get<IPagination>(this.baseUrl + 'products', {observe: 'response', params})
      // HttpResponse<IPagination> => map HttpResponse to IPagination
      .pipe(
        map(response => {
          return response.body; // response.body is IPagination property.
        })
      );
  }
  // product-details.component.ts için, yani tek bir product için
  getProduct(id: number) {
    return this.http.get<IProduct>(this.baseUrl + 'products/' + id);
  }

  getBrands() {
      return this.http.get<IBrand[]>(this.baseUrl + 'products/brands');
  }

  getTypes() {
    return this.http.get<IType[]>(this.baseUrl + 'products/types');
}
}
