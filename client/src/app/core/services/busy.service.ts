import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
 busyRequestCount = 0;
 // sayfadaki load animasyonu için
  constructor(private spinnerService: NgxSpinnerService) { }

  busy() {
    this.busyRequestCount++;
    this.spinnerService.show(undefined, {
    // type: 'ball-grid-beat',
      type: 'square-jelly-box',
    //  type: 'timer',
      bdColor: 'rgba(255,255,255,0.7)',
      color: '#333333',
      size: 'default'
    });
  }

  idle() {
    this.busyRequestCount--;
    if (this.busyRequestCount <= 0) {
      this.busyRequestCount = 0;
      this.spinnerService.hide();
    }
  }
  // daha sonra loaging.interceptors.ts de metodları kullanacagız
}
