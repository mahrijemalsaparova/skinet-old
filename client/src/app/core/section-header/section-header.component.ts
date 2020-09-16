import { Observable } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-section-header',
  templateUrl: './section-header.component.html',
  styleUrls: ['./section-header.component.scss']
})
export class SectionHeaderComponent implements OnInit {
  breadcrumb$: Observable<any[]>;


  constructor(private bcService: BreadcrumbService) { }

  ngOnInit() {
    // mini menunun sağ kısmındaki bölümü headerda göstermek için
    // daha sonra section-header.component.html e giderek etkinleştirdik.
    this.breadcrumb$ = this.bcService.breadcrumbs$;
  }

}
