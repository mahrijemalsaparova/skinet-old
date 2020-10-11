import { AccountService } from './account/account.service';
import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
// each Component has a decorator like this
// angular componenti olduğunu belli etmek için
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'Skinet'; // app.component.html deki title yerine yazar

  constructor(private basketService: BasketService, private accountService: AccountService) {}

  ngOnInit(): void {
    this.loadBasket();
    this.loadCurrentUser();
    }

  loadCurrentUser() {
    // sayfa refresh olduğunda login olan user aynen kalacak yok olmayacak
    const token = localStorage.getItem('token');
    if (token) {
      this.accountService.loadCurrentUser(token).subscribe(() => {
        console.log('loaded user');
      }, error => {
        console.log(error);
      });
    }
  }

  loadBasket() {
    // sayfa refresh olduğunda baskete eklenen itemler aynen kalacak yok olmayacak
    const basketId = localStorage.getItem('basket_id');
    if (basketId) {
      this.basketService.getBasket(basketId).subscribe(() => {
        console.log('initialised basket');
      }, error => {
        console.log(error);
      });
    }
  }
}
