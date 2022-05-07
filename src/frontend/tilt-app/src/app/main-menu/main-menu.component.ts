import { Component, OnInit } from '@angular/core';
import { MenuController } from '@ionic/angular';

@Component({
  selector: 'app-main-menu',
  templateUrl: './main-menu.component.html',
  styleUrls: ['./main-menu.component.scss'],
})
export class MainMenuComponent {

  constructor(private menu: MenuController) { }


  openCustom() {
    this.menu.enable(true, 'custom');
    this.menu.open('custom');
  }


  


}
