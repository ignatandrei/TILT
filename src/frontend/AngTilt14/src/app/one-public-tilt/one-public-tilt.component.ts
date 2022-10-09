import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
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

  totalNumberOfTILTS: number|null = null;
  activeDayIsOpen: boolean = true;
  refresh = new Subject<void>();
  viewDate: Date = new Date();
  view: CalendarView= CalendarView.Month;
  events: CalendarEvent[] = [];
  maxObj:TILT|null=null;
  profileForm =  new FormGroup({
    url: new FormControl(''),
    publicTILTS: new FormArray([new FormControl(new TILT())])
  });
  

  constructor(private publicService:PublicTiltsService,private route: ActivatedRoute, private fb:FormBuilder, private clipboard: Clipboard) { 
      
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
    var self=this;
    var id:string="id";
    this.route.params.pipe(
       tap(it=>console.log('received',it)),
       filter(it=>it[id] != null),
       map(it=>it[id]),
      tap(it => this.profileForm.patchValue({url:it})),
      tap(it => this.publicService.nrTilts(it).subscribe(a=> self.totalNumberOfTILTS = a.nrTilts)),      
       tap(it  => console.log("id is ",it)),
       switchMap(it => this.publicService.getTilts(it,100000)),
       tap(it  => console.log("tilts are ",it))

    ).subscribe(
      it=>{
        it=it.sort((b,a)=>b.forDate!.localeCompare(a.forDate!));
        // console.log('patching valyues', it);
        // this.profileForm.patchValue({publicTILTS: [...it]});
        //setControl<K extends string & keyof TControl>
        this.profileForm.setControl("publicTILTS",new FormArray(it.map(a=>new FormControl(a))));
        console.log('patching values', it, this.profileForm.value.publicTILTS);
        it.forEach((a,index, arr)=> {                      
          a.numberOfDaysStreak = 1;          
          if(index>0){

            a.prevTilt = arr[index-1];

            if(a.existsPrevStreak)
              a.numberOfDaysStreak = arr[index-1].numberOfDaysStreak+1;              
          }
          
        });
        if(it.length>0){
            this.maxObj = it.reduce((prev, current) => (prev.numberOfDaysStreak > current.numberOfDaysStreak) ? prev : current)
          this.maxObj.isMax = true;
          this.maxObj.MaxDaysInStreak=this.maxObj.numberOfDaysStreak;
          this.maxObj.isPartOfMax= true;
          while(this.maxObj.prevTilt){
            this.maxObj.prevTilt.isPartOfMax=true;
            this.maxObj.prevTilt.MaxDaysInStreak=this.maxObj.MaxDaysInStreak;
            this.maxObj = this.maxObj.prevTilt;
          }
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
  private weekTilts(nr:number): string{
    var str="TILTS for week "+nr;
    if(this.profileForm.value.publicTILTS == null)
      return '';

    var tilts = this.profileForm.value.publicTILTS
      .map(it=>it as TILT)
    .filter(it=> it != null && it.WeekNumber==nr)
    .map(it=>`TILT for ${it.LocalDateStringNoTime} => ${it.text}  ${it.link}` )
    .join('\n');
    ;
    str += '\n'+tilts;
    str += '\n See my tilts at '+ environment.url+'AngTilt/tilt/public/'+this.profileForm.controls['url'].value;
    return str;
  }
  copyWeek(nr: number | undefined): void{

    if(nr === undefined)
      return;    
    this.copyToClipboard(this.weekTilts(nr));
    return ;
  }
  shareWeek(nr: number| undefined): void{
    if(nr === undefined)
      return;    

    if(!this.share(this.weekTilts(nr)))
    {
      window.alert('sorry, share not available ... will improve');
    }
    return ;
  }
    private shareOrCopy(str: string){

    if(!this.share(str))
      this.copyToClipboard(str);
  }
  private share(str:string): boolean{

    if ('share' in navigator) {
      navigator
      .share({
        title: 'TILT!',
        text: str,
        url: environment.url + 'AngTilt/tilt/public/'+this.profileForm.controls['url'].value
      })
      .then(() => {
        console.log('Callback after sharing');
      })
      .catch(console.error);
      return true;
    } else {
      return false;
    }
  }
  private copyToClipboard(str:string){

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
    
  }

  
}
