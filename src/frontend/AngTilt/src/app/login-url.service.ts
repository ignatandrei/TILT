import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoginUrlService {

  public isLoggedIn:boolean = false;
  public redirectUrl :string = '';
  
  constructor() { }
}
