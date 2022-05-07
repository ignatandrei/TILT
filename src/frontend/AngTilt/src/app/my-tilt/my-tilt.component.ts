import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { catchError, of, switchMap, tap } from 'rxjs';
import { TILT } from '../classes/TILT';
import { LoginUrlService } from '../services/login-url.service';
import { formatDistance, subDays } from 'date-fns'

@Component({
  selector: 'app-my-tilt',
  templateUrl: './my-tilt.component.html',
  styleUrls: ['./my-tilt.component.css']
})
export class MyTiltComponent implements OnInit {

  profileForm = this.fb.group({
    hasTodayTilt: false,
    lastTilt:null,
    nextTiltSeconds:null,
    nextTilt:this.fb.group({
      text:['', [Validators.required, Validators.maxLength(100)]]
    })
  });

  constructor(private myTiltService:LoginUrlService, private fb:FormBuilder) { }

  ngOnInit(): void {
    this.myTiltService.HasTILTToday().subscribe(hasTodayTilt=> this.profileForm.patchValue(
      {
        hasTodayTilt : hasTodayTilt
      }

    ));
    var nextDate=new Date();
    nextDate.setDate(nextDate.getDate()+1);
    nextDate=new Date(nextDate.setHours(0,0,0,0));
    this.myTiltService.LastTilt().subscribe(it=>{
      this.profileForm.patchValue({
        lastTilt:it, 
        nextTiltSeconds: (it != null)? formatDistance(nextDate, it.TheDate, { addSuffix: true }) : null
      });
      
    });
  }

  saveTILT():void{
    //window.alert('save tilt' + this.profileForm.value.nextTilt.text) ;
    var t=new TILT(this.profileForm.value.nextTilt);
    window.alert('save tilt' +JSON.stringify(t));
    this.myTiltService.addTILT(t)
    .pipe(
      tap(it=> {
        this.profileForm.patchValue({
          lastTilt:it,
          hasTodayTilt:true
      }
    )}),
      catchError(it=>
        {
          window.alert("error "+ it);
          throw it;
      })
    )
    .subscribe();
  }

}
