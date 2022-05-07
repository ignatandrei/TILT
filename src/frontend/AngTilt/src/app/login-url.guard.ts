import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { LoginUrlService } from './login-url.service';

@Injectable({
  providedIn: 'root'
})
export class LoginUrlGuard implements CanActivate {
  
  constructor(private loginService: LoginUrlService, private router: Router){

  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      if(this.loginService.isLoggedIn) return true;
      this.loginService.redirectUrl=state.url;
      return this.router.parseUrl('/loginURL');
      
  }

  
}
