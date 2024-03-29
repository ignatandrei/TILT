import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, Validators } from '@angular/forms';
import { catchError, last, of, switchMap, tap, zip } from 'rxjs';
import { TILT } from '../classes/TILT';
import { LoginUrlService } from '../services/login-url.service';
import { formatDistance, subDays ,differenceInHours, formatDistanceToNowStrict} from 'date-fns'

@Component({
  selector: 'app-my-tilt',
  templateUrl: './my-tilt.component.html',
  styleUrls: ['./my-tilt.component.css']
})
export class MyTiltComponent implements OnInit {

  profileForm = this.fb.group({
    hasTodayTilt: false,
    mainUrl:'',
    lastTilt:new TILT(null),
    nextTiltSecondsString:'',
    nextTilt:this.fb.group({
      text:['', [Validators.required, Validators.maxLength(100)]],
      link:['', [Validators.maxLength(500)]],
    })
  });

  get f(): { [key: string]: AbstractControl } {
    return this.profileForm.controls;
  }
  get lastTiltNotNull():TILT{
    return this.profileForm.value.lastTilt ?? new TILT(null);
  }

  public HasError(key:string,name:string):boolean{
    var cnt =this.f[key];
    if(cnt == null)
      return true;
     
    if(cnt.errors == null)
      return true;

      if(cnt.errors[name] != null)
        return true;

        return false;
  }
  public logOff(){
    this.authUrl.LogOff().subscribe(it=>{
      if(it){
        //console.log('exit');
        window.location=window.location;
      }
    });
  }
  constructor(private myTiltService:LoginUrlService, private fb:FormBuilder, private authUrl: LoginUrlService) { }

  patchData(hasTodayTilt:boolean, mainUrl:string, lastTilt:TILT|null):void{
    var nextDate=new Date();
    nextDate.setDate(nextDate.getDate()+1);
    nextDate=new Date(nextDate.setHours(0,0,0,0));
    var sec : string|null = null;
      if(lastTilt!=null){
        
        var localDateStart=lastTilt.LocalJustDate;
        var day = 60 * 60 * 24 * 1000;
        nextDate=new Date(localDateStart.getTime()+day);
        
        console.log('local', localDateStart);
        console.log('next',nextDate);
        console.log('tilt',lastTilt.LocalJustDate);
        
        var h = differenceInHours(nextDate,new Date());
        console.log("diff",h);
        sec=formatDistance(nextDate,new Date(), { addSuffix: true }) ;
        if(nextDate<new Date()){
          //todo: fix privateHasTILTToday
          //hasTodayTilt=false;
        }
        if(h<2 ){
          sec=formatDistanceToNowStrict(nextDate, { addSuffix: true, unit: 'minute' }) ;
          console.log('here',sec);
        }

        //console.log(sec);
      }
      var nextTiltSecondsString:string|null = null;
      console.log('hasTodayTilt', hasTodayTilt);
      console.log('mainUrl', mainUrl);
      // debug purposes : hasTodayTilt=false;
      this.profileForm.patchValue(
        {
          hasTodayTilt : hasTodayTilt,
          mainUrl:mainUrl,
          lastTilt:lastTilt, 
          nextTiltSecondsString: sec
        }
      
      );
  
  }
  ngOnInit(): void {

    var tiltToday$=this.myTiltService.HasTILTToday();
    var mainUrl$ = this.myTiltService.MyUrl(); 
    var lastTilt$= this.myTiltService.LastTilt()
    var obs=zip(tiltToday$, mainUrl$, lastTilt$)
    .pipe(
      // tap(([hasTodayTilt,mainUrl,lastTilt])=> this.patchData(hasTodayTilt,mainUrl,lastTilt)),
    )
    .subscribe(([hasTodayTilt,mainUrl,lastTilt])=> this.patchData(hasTodayTilt,mainUrl,lastTilt));

      
      
    
  }

  saveTILT():void{
    //window.alert('save tilt' + this.profileForm.value.nextTilt.text) ;
    var t=new TILT(this.profileForm.value.nextTilt);
    //window.alert('save tilt' +JSON.stringify(t));
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
