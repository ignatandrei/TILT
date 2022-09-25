import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AmsService } from '../ams/ams.service';
import { AMSData } from '../ams/AMSData';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.css']
})
export class AboutComponent implements OnInit {

  ams: AMSData|undefined=undefined;
  public urlAPI: string=''; 

  constructor(private amsService: AmsService) { 
      this.urlAPI= environment.url;
  }

  ngOnInit(): void {
    this.amsService.AmsDataValues().subscribe(
      it=>{
        console.log('amsService.AmsDataValues()',it);
        //console.log('amsService.AmsDataValues()',it.length);
        this.ams=it.sort((a,b)=>b!.TheDate!.getDate()-a!.TheDate!.getDate())?.pop();
      });
  }

}
