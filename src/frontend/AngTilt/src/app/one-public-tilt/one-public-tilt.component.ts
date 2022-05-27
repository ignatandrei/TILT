import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CalendarEvent, CalendarView } from 'angular-calendar';
import { startOfDay,format} from 'date-fns';
import { filter, map, Subject, switchMap, tap } from 'rxjs';
import { TILT } from '../classes/TILT';
import { PublicTiltsService } from '../services/public-tilts.service';
import { Clipboard } from '@angular/cdk/clipboard';
import { environment } from 'src/environments/environment';
const colors: any = {
  red: {
    primary: '#ad2121',
    secondary: '#FAE3E3',
  },
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF',
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA',
  },
};
@Component({
  selector: 'app-one-public-tilt',
  templateUrl: './one-public-tilt.component.html',
  styleUrls: ['./one-public-tilt.component.css']
})
export class OnePublicTiltComponent implements OnInit {

  activeDayIsOpen: boolean = true;
  refresh = new Subject<void>();
  viewDate: Date = new Date();
  view: CalendarView= CalendarView.Month;
  events: CalendarEvent[] = [];
  maxObj:TILT|null=null;
  profileForm = this.fb.group({
    url: [''],
    publicTILTS: this.fb.array([])
  });
  

  constructor(private publicService:PublicTiltsService,private route: ActivatedRoute, private fb:FormBuilder, private clipboard: Clipboard) { 

  }
  get tiltsFormArray(): FormArray{
    return this.profileForm.get('publicTILTS') as FormArray;
}
closeOpenMonthViewDay(): void{
  this.activeDayIsOpen=false;
}
dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    this.viewDate = date;
}

  gotoMaxStreak():void{
    if(!this.maxObj)
      return;
    this.viewDate = this.maxObj.LocalJustDate;
    this.refresh.next();
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
        it=it.sort((b,a)=>b.forDate!.localeCompare(a.forDate!));
        // this.tiltsFormArray.push(...it.map(it=>this.fb.control(it)));
        it.forEach(a=>this.tiltsFormArray.push(this.fb.control(a)));
        // this.tiltsFormArray.push(new TILT());
        it.forEach((a,index, arr)=> {

          a.existsPrev =arr.findIndex(b=> (b.NextJustDate.getDate()  ==  a.LocalJustDate.getDate()) ) != -1 ;
          a.existsNext =arr.findIndex(b=> (b.PrevJustDate.getDate()  ==  a.LocalJustDate.getDate()) ) != -1 ;
          if(index>0){

            a.prevTilt = arr[index-1];

            if(a.existsPrev)
              a.numberOfDays = arr[index-1].numberOfDays+1;
          }
          
        });
        this.maxObj = it.reduce((prev, current) => (prev.numberOfDays > current.numberOfDays) ? prev : current)
        
        this.maxObj.isMax = true;
        this.maxObj.MaxDaysInStreak=this.maxObj.numberOfDays;
        this.maxObj.isPartOfMax= true;
        while(this.maxObj.prevTilt){
          this.maxObj.prevTilt.isPartOfMax=true;
          this.maxObj.prevTilt.MaxDaysInStreak=this.maxObj.MaxDaysInStreak;
          this.maxObj = this.maxObj.prevTilt;
        }

        it.forEach(a=> this.events.push(
          {
            start:  a.LocalJustDate,
            title: a.text||'',
            color: colors.red,
            allDay: true,
            draggable: false,
            meta:{
              originalItem:a  
            }
          }
        ));
          this.refresh.next();
      }
    );

  }
  copyWeek(nr: number): void{

    var str="TILTS for week "+nr;
    var tilts = this.tiltsFormArray.controls
      .map(it=>it.value as TILT)
    .filter(it=> it != null && it.WeekNumber==nr)
    .map(it=>`TILT for ${format(it.LocalDate,'dd MMM yyyy')} => ${it.text}`)
    .join('\n');
    ;
    str += '\n'+tilts;
    str += '\n See my tilts at '+ environment.url+'AngTilt/public/'+this.profileForm.controls['url'].value;
    const pending = 
            this.clipboard.beginCopy(str);
    let remainingAttempts = 3;
    const attempt = () => {
      const result = pending.copy();
      if (!result && --remainingAttempts) {
        setTimeout(attempt);
      } else {
        window.alert('Copied to clipboard');
        pending.destroy();
      }
    };
    attempt();
    return ;
  }
  
}
