import { delay, map, Observable, scan, tap } from "rxjs";
import { ajax } from 'rxjs/ajax';
import { publicTilt } from "../classes/publicTilt";
import { TILT } from "../classes/TILT";

class JsonStreamDecoder {
  /** item starts and ends at level 0 */
  private level = 0;

  /** when an item is split in two */
  private partialItem = '';

  private decoder = new TextDecoder();

  public decodeChunk<T>(
    value: Uint8Array,
    decodedItemCallback: (item: T) => void
  ): void {
    const chunk = this.decoder.decode(value);
    let itemStart = 0;

    for (let i = 0; i < chunk.length; i++) {
      if (chunk[i] === JTOKEN_START_OBJECT) {
        if (this.level === 0) {
          itemStart = i;
        }
        this.level++;
      }
      if (chunk[i] === JTOKEN_END_OBJECT) {
        this.level--;
        if (this.level === 0) {
          let item = chunk.substring(itemStart, i + 1);
          if (this.partialItem) {
            item = this.partialItem + item;
            this.partialItem = '';
          }
          decodedItemCallback(JSON.parse(item));
        }
      }
    }
    if (this.level !== 0) {
      this.partialItem = chunk.substring(itemStart);
    }
  }
}
const JTOKEN_START_OBJECT = '{';
const JTOKEN_END_OBJECT = '}';


export class PublicTiltsService {

    baseUrl:string = '';
    constructor() { 
      this.baseUrl=process.env.REACT_APP_URL+'api/'; 
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
        return this.fromFetchStream<TILT>(this.baseUrl+'PublicTILTs/LatestTILTs/'+id + '/'+nr)
        .pipe(
          tap(it=>console.log('received',it)),
          map(it=>new TILT(it)),
          scan((acc,value)=>[...acc, value], [] as TILT[])
        );  
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
          map(it=>new publicTilt({url:id,nrTilts: it})),
          delay(5000),
        );    
      ;
  
    }


     //https://gist.github.com/markotny/d21ef4e1af3d6ea5332b948c9c9987e5
  //https://medium.com/@markotny97/streaming-iasyncenumerable-to-rxjs-front-end-8eb5323ca282
  public fromFetchStream<T>(input: RequestInfo, init?: RequestInit): Observable<T> {
    return new Observable<T>(observer => {
      const controller = new AbortController();

      fetch(input, { ...init, signal: controller.signal })
        .then(async response => {
          const reader = response.body?.getReader();
          if (!reader) {
            throw new Error('Failed to read response');
          }
          const decoder = new JsonStreamDecoder();

          while (true) {
            const { done, value } = await reader.read();
            if (done) break;
            if (!value) continue;

            decoder.decodeChunk<T>(value, item => observer.next(item));
          }
          observer.complete();
          reader.releaseLock();
        })
        .catch(err => observer.error(err));

      return () => controller.abort();
    });
  }
  
}  