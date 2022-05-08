import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { catchError, of, switchMap, tap } from 'rxjs';
import { TILT } from '../classes/TILT';
import { LoginUrlService } from '../services/login-url.service';
import { formatDistance, subDays ,differenceInHours} from 'date-fns'

@Component({
  selector: 'app-my-tilt',
  templateUrl: './my-tilt.component.html',
  styleUrls: ['./my-tilt.component.css']
})
export class MyTiltComponent implements OnInit {

  profileForm = this.fb.group({
    hasTodayTilt: false,
    mainUrl:'',
    lastTilt:null,
    nextTiltSecondsString:null,
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


    this.myTiltService.MyUrl().subscribe(url=> this.profileForm.patchValue({
      mainUrl:url
    }));

    var nextDate=new Date();
    nextDate.setDate(nextDate.getDate()+1);
    nextDate=new Date(nextDate.setHours(0,0,0,0));
    
    this.myTiltService.LastTilt().subscribe(it=>
      {

      var sec : string|null = null;
      if(it!=null){
        var localDateStart=new Date(it.LocalDate.setHours(0,0,0,0));
        console.log('local', localDateStart);
        console.log('next',nextDate);
        var h = differenceInHours(nextDate,localDateStart);
        console.log("diff",h);
        if(h>24){

        }

        sec=formatDistance(nextDate, new Date(), { addSuffix: true }) ;

        //console.log(sec);
      }
      this.profileForm.patchValue({
        lastTilt:it, 
        nextTiltSecondsString: sec
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
