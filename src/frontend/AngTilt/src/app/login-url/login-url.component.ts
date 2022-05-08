import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { catchError, of, switchMapTo } from 'rxjs';
import { LoginUrlService } from '../services/login-url.service';

@Component({
  selector: 'app-login-url',
  templateUrl: './login-url.component.html',
  styleUrls: ['./login-url.component.css']
})
export class LoginUrlComponent implements OnInit {

  profileForm = this.fb.group({
    urlPath: ['test', Validators.required],
    secret: ['test', Validators.required],
    
  });
  constructor(private fb: FormBuilder, private authUrl: LoginUrlService, private router: Router ) { }

  ngOnInit(): void {
    
  }
  onSubmit() {
    this.authUrl.LoginOrCreate(this.profileForm.value.urlPath, this.profileForm.value.secret)
    .pipe(
      catchError((err,caught)=>{
        window.alert('please try again');
        return of('');
      }
    )
    )
    .subscribe(it=>{
        this.router.navigate([this.authUrl.redirectUrl]);
      
    }, 
    )
  }

}
