import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { filter, map, switchMap, tap } from 'rxjs';
import { TILT } from '../classes/TILT';
import { PublicTiltsService } from '../services/public-tilts.service';

@Component({
  selector: 'app-one-public-tilt',
  templateUrl: './one-public-tilt.component.html',
  styleUrls: ['./one-public-tilt.component.css']
})
export class OnePublicTiltComponent implements OnInit {

  profileForm = this.fb.group({
    url: [''],
    publicTILTS: this.fb.array([])
  });
  

  constructor(private publicService:PublicTiltsService,private route: ActivatedRoute, private fb:FormBuilder) { 

  }
  get tiltsFormArray(): FormArray{
    return this.profileForm.get('publicTILTS') as FormArray;
}

  ngOnInit(): void {
    var id:string="id";
    this.route.params.pipe(
       tap(it=>console.log('received',it)),
       filter(it=>it[id] != null),
       map(it=>it[id]),
      tap(it => this.profileForm.controls['url'].setValue(it)),
       tap(it  => console.log("id is ",it)),
       switchMap(it => this.publicService.getTilts(it,100000)),
       tap(it  => console.log("tilts are ",it))

    ).subscribe(
      it=>{
        this.tiltsFormArray.clear();
        it=it.sort((a,b)=>b.forDate!.localeCompare(a.forDate!));
        // this.tiltsFormArray.push(...it.map(it=>this.fb.control(it)));
        it.forEach(it=>this.tiltsFormArray.push(this.fb.control(it)));
        // this.tiltsFormArray.push(new TILT());
        
      }
    );
  }

}
