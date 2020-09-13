import { Component, OnInit } from '@angular/core';
// each Component has a decorator like this
// angular componenti olduğunu belli etmek için
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'Skinet'; // app.component.html deki title yerine yazar

  constructor() {}

  ngOnInit(): void {
    }
}
