import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { TILT } from '../classes/tilt';

@Injectable({
  providedIn: 'root'
})
export class PublicTiltsService {

  baseUrl:string = '';
  constructor(private http: HttpClient) { 
    this.baseUrl = environment.url;

  }

  public getUrls():Observable<string[]>{
    return this.http.get<string[]>(this.baseUrl + 'PublicTILTs/PublicTiltsURL');
  }

  public getTilts(id:string, nr:number): Observable<TILT[]>{
  //  var arr: TILT[] = [];
  //  var t=new TILT();
  //  t.id=1;
  //  t.text="my new tilt";
  //  t.forDate="2020-01-01";
  //  t.idurl=1;
  //   arr.push(t);
  //   t=new TILT();
  //  t.id=2;
  //  t.text="my new tilt";
  //  t.forDate="2021-01-01";
  //  t.idurl=1;
  //   arr.push(t);    
    // return of(arr)

    return this.http.get<TILT[]>(this.baseUrl+'PublicTILTs/LatestTILTs/' + id + '/' + nr);
  }
}
