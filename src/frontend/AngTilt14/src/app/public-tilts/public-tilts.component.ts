import { Component, OnInit } from '@angular/core';
import { PublicTiltsService } from '../services/public-tilts.service';
import { FormArray, FormBuilder } from '@angular/forms';
import { publicTilt } from './publicTilt';
@Component({
  selector: 'app-public-tilts',
  templateUrl: './public-tilts.component.html',
  styleUrls: ['./public-tilts.component.css']
})
export class PublicTiltsComponent implements OnInit {

    profileForm = this.fb.group({
      publicUrls: this.fb.array([
        this.fb.control(new publicTilt())
      ])
    });
    
  constructor(private publicService:PublicTiltsService, private fb:FormBuilder) { 

    

  }
  get publicUrls() {
    return this.profileForm.get('publicUrls') as FormArray;
  }

  ngOnInit(): void {
    this.publicService.getUrls().subscribe(data=>{
      // window.alert(JSON.stringify(data));
      this.publicUrls.clear();
      // data.forEach(it=> this.publicUrls.push(this.fb.control(new publicTilt({url:it}))));
      data.forEach(it=>{
         this.publicService.nrTilts(it).subscribe(a=> 
          {
            console.log("obtaining ", a );            
            this.publicUrls.push(this.fb.control(a));            
          }
          );
        });

    });
  }

}
