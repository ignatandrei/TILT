import { Component } from '@angular/core';
import { PublicTiltsService } from '../services/public-tilts.service';
import { TILT } from '../classes/tilt'

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage {

  public allPublicTilts: string[];
  constructor(
    private publicTiltsService: PublicTiltsService 
  ) {}

    ngOnInit() {
      this.publicTiltsService.getUrls().subscribe(data => {
        
        this.allPublicTilts = data;
        
      })
    }


}
