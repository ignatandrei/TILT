import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { of, switchMap, tap } from 'rxjs';
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
    nextTilt:null,
    publicUrls: this.fb.array([
      this.fb.control({url:'please wait'})
    ])
  });

  constructor(private myTiltService:LoginUrlService, private fb:FormBuilder) { }

  ngOnInit(): void {
    this.myTiltService.HasTILTToday().subscribe(hasTodayTilt=> this.profileForm.patchValue(
      {
        hasTodayTilt : hasTodayTilt
      }

    ));

    this.myTiltService.LastTilt().subscribe(it=>{
      this.profileForm.patchValue({
        lastTilt:it,
        nextTiltSeconds: (it != null)? formatDistance(new Date(),new Date(it.TheDate.setHours(0,0,0,0)), { addSuffix: true }) : null
      });
      
    });
  }

}
