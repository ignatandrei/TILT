import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { map, Observable } from 'rxjs';
import { LoginUrlService } from './services/login-url.service';

@Injectable({
  providedIn: 'root'
})
export class LoginUrlGuard implements CanActivate {
  
  constructor(private loginService: LoginUrlService, private router: Router){

  }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      
      this.loginService.redirectUrl=state.url;
      
      var s:Observable<boolean | UrlTree>= this.loginService.isLoggedIn()
        .pipe(          
            map(it=> {
              if(it) return true;
              return this.router.parseUrl('/loginURL');
      
            })
          )
      ;
      return s;
      
}

  
}
