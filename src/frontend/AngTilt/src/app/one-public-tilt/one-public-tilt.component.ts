import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CalendarEvent } from 'angular-calendar';
import { startOfDay } from 'date-fns';
import { filter, map, Subject, switchMap, tap } from 'rxjs';
import { TILT } from '../classes/TILT';
import { PublicTiltsService } from '../services/public-tilts.service';

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
  events: CalendarEvent[] = [];

  profileForm = this.fb.group({
    url: [''],
    publicTILTS: this.fb.array([])
  });
  

  constructor(private publicService:PublicTiltsService,private route: ActivatedRoute, private fb:FormBuilder) { 

  }
  get tiltsFormArray(): FormArray{
    return this.profileForm.get('publicTILTS') as FormArray;
}

dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    this.viewDate = date;
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
        it.forEach(a=>this.tiltsFormArray.push(this.fb.control(a)));
        // this.tiltsFormArray.push(new TILT());

        it.forEach(a=> this.events.push(
          {
            start:  startOfDay(a.LocalDate),
            title: a.text||'',
            color: colors.red,
            allDay: true,
            draggable: false
          }
        ));
          this.refresh.next();
      }
    );
  }

}
