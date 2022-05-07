import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-login-url',
  templateUrl: './login-url.component.html',
  styleUrls: ['./login-url.component.css']
})
export class LoginUrlComponent implements OnInit {

  profileForm = this.fb.group({
    urlPath: ['', Validators.required],
    secret: ['', Validators.required],
    
  });
  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    
  }
  onSubmit() {
    console.log(this.profileForm.value);
  }

}
