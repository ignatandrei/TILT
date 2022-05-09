import { Component } from '@angular/core';
import { PublicTiltsService } from '../services/public-tilts.service';
import { TILT } from '../classes/tilt'
import { latestUrlTitls } from '../classes/latestUrlTilts';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage {

  public allPublicTilts: latestUrlTitls[] = [];
  public latestTilts: TILT[];
  constructor(
    private publicTiltsService: PublicTiltsService 
  ) {}

    ngOnInit() {
      this.getPublicURL();
    }

    getPublicURL() {
      this.publicTiltsService.getUrls().subscribe(data => {
        this.allPublicTilts = data.map(it => new latestUrlTitls(it));
        console.log(this.allPublicTilts);
        for(let i = 0; i < this.allPublicTilts.length; i++) {
          let item = this.allPublicTilts[i];
          this.publicTiltsService.getTilts(item.url, 1).subscribe(data => {
            console.log(data);
            item.arrayOfTilts = data;
          })
          
        }
      })
    }


}
