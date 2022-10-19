import { map, Observable, tap } from "rxjs";
import { ajax } from 'rxjs/ajax';
import { publicTilt } from "../classes/publicTilt";

export class PublicTiltsService {

    baseUrl:string = '';
    constructor() { 
      this.baseUrl=process.env.REACT_APP_URL+'api/'; 
    }
  
    public getUrls():Observable<string[]>{
      return ajax.get<string[]>(this.baseUrl+'PublicTILTs/PublicTiltsURL')
        .pipe(
          map(it=> it.response)
        )
    }
    public nrTilts(id:string): Observable<publicTilt>{
      const options = {
        responseType: 'text' as const,
      };
      return ajax.get<string>(this.baseUrl+`PublicTILTs/CountTILTs/${id}/count`,options)
        .pipe(        
          tap(it=>console.log(`for ${id} number of tilts is ${it.response}`)),
          map(it=> isNaN(+it.response)? null: +it.response),
          map(it=>new publicTilt({url:id,nrTilts: it}))
        );    
      ;
  
    }
  
}  