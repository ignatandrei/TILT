import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, of, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { AMSData, ReceivedAMS } from './AMSData';

@Injectable({
  providedIn: 'root'
})
export class AmsService {

  baseUrl:string = '';
  constructor(private http: HttpClient) { 
    this.baseUrl=environment.url;

  }
  public AmsDataValues():Observable<AMSData[]>{
    var url=this.baseUrl+'ams/all';
    return this.http.get<ReceivedAMS[]>(url)
      .pipe
      (
        tap(it=>console.log('received',it)),
        map(it=> it.map(a=> {
            var x= new AMSData(a.value);
            x.urlOfData = url;
            return x;
        }))
      );
  }
}


