import { BusyService } from './../services/busy.service';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { delay, finalize } from 'rxjs/operators';

@Injectable()
// anasayfadki loading animasyonu için
export class LoadingInterceptor implements HttpInterceptor {
    constructor(private busyService: BusyService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        this.busyService.busy();
        return next.handle(req).pipe(
            delay(1000),
            // eğer request bittiyse
            finalize(() => {
                this.busyService.idle();
            })
        );
    }
    // daha sonra bunu aktive etmek için app.module gidip tanımlıcaz

}
