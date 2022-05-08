import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { catchError, map, Observable, of, switchMap, switchMapTo, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { TILT } from '../classes/TILT';
import { BrowserStorageService } from '../general/storage/browseStorage';

@Injectable({
  providedIn: 'root'
})
export class LoginUrlService {

  sessionName="SESSION_ID";
  private get jwt(): string {
    var val =  this.storage.get(this.sessionName) ||'';      
    return val;

  }
  private set jwt(val:string){
    this.storage.set(this.sessionName, val);
  }
  private get wasLoggedIn():boolean {
    return this.jwt.length>0;
  };
  public MyUrl():Observable<string>{
    if (this.jwt.length == 0)
      return of('');

    return this.http.get<string>(this.baseUrl+'TILT/MainUrl',{
        headers: new HttpHeaders(
          {
            'Authorization': 'CustomBearer ' + this.jwt,
             'Content-Type': 'application/json'
          }),        
        responseType: 'text' as 'json'})
    
  }
  public addTILT(tilt:TILT):Observable<TILT>{
    return this.http.post<TILT>(this.baseUrl+'TILT/AddTILT', tilt, {
      headers: new HttpHeaders(
        {
          'Authorization': 'CustomBearer ' + this.jwt,
           'Content-Type': 'application/json'
        })
      }      
      ).pipe
      (
        map(it=>new TILT(it))
      );
  }

  public  isLoggedIn():Observable<boolean> {
    if (this.jwt.length == 0)
      return of(false);

    return this.http.get<boolean>(this.baseUrl+'AuthAll/IsUserAuthenticated',{
        headers: new HttpHeaders(
          {
            'Authorization': 'CustomBearer ' + this.jwt,
             'Content-Type': 'application/json'
          }),        
        responseType: 'text' as 'json'})
      .pipe(
        catchError((err:any,caught:Observable<boolean>)=>{
          console.log('auth failed',err);
          return of(false);
        }
      ));
  };
  
  public redirectUrl :string = 'tilt/my';
  baseUrl:string = '';
  constructor(private http: HttpClient, private storage: BrowserStorageService) { 
    this.baseUrl=environment.url;
  }
  
  public HasTILTToday():Observable<boolean>{
    if(!this.wasLoggedIn)return of(false);

    return this.http.get<boolean>(this.baseUrl+'TILT/HasTILTToday', {
      headers: new HttpHeaders(
        {
          'Authorization': 'CustomBearer ' + this.jwt,
           'Content-Type': 'application/json'
        }),        
      responseType: 'text' as 'json'});


  }
  public LastTilt():Observable<TILT|null>{
    
    if(!this.wasLoggedIn)return of(null);

    return this.http.get<TILT[]>(this.baseUrl+'TILT/MyLatestTILTs/1', {
      headers: new HttpHeaders(
        {
          'Authorization': 'CustomBearer ' + this.jwt,
           'Content-Type': 'application/json'
        }),        
      })
      
      .pipe(
        map(tilt=>{
          if(tilt == null)
              return null;     
          if(tilt.length == 0) 
            return null;    
          return new TILT(tilt[0]);
        })
      );
      


  }

  private realLoginLoginOrCreate(urlPart: string, secret:string):Observable<string>{ 
    return this.http.get<string>(this.baseUrl+'AuthAll/CreateEndPoint/'+urlPart+'/'+secret, {responseType: 'text' as 'json'})
    .pipe(
      tap(it=> {
        this.jwt=it;
      }),
      catchError((err:any,caught:Observable<string>)=>{
          console.log('auth failed',err);
          this.jwt='';
          throw err;
      }),

    );

  }
  public LoginOrCreate(urlPart: string, secret:string):Observable<string>{

    if(this.wasLoggedIn){
      return this.isLoggedIn()
       .pipe(
         map(it=> {
           if(it)return this.jwt; 
           this.jwt='';
           return '';
         })
         ,
         switchMap(it=>{
           if(it.length>0)return of(it);
            return this.realLoginLoginOrCreate(urlPart, secret);
         })
        
       );
       
     }

     return this.realLoginLoginOrCreate(urlPart, secret);

    }
}
