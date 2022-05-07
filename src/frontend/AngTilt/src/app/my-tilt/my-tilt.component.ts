import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { LoginUrlService } from '../services/login-url.service';

@Component({
  selector: 'app-my-tilt',
  templateUrl: './my-tilt.component.html',
  styleUrls: ['./my-tilt.component.css']
})
export class MyTiltComponent implements OnInit {

  profileForm = this.fb.group({
    hasTodayTilt: this.fb.control(false),
    publicUrls: this.fb.array([
      this.fb.control({url:'please wait'})
    ])
  });

  constructor(private myTiltService:LoginUrlService, private fb:FormBuilder) { }

  ngOnInit(): void {
    this.myTiltService.HasTILTToday().subscribe(it=>{
      this.profileForm.value.hasTodayTilt =it;
    })
  }

}
