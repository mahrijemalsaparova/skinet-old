import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {
// parent (shop.component.ts) componentden child componente(product-item.ts) veri çekmek için
@Input() product: IProduct;
  constructor() { }

  ngOnInit(): void {
  }

}
