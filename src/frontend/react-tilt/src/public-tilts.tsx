import React, { useEffect, useState } from 'react';
import './App.css';
import { publicTilt } from './classes/publicTilt';
import { PublicTiltsService } from './services/public-tilts.service';

function PublicTilts() {
    const [publicTilts, addTilts] = useState(Array<publicTilt>(0));
    
    useEffect(() => {
        console.log("hello");
        return () => console.log("goodbye");
      }, []);
      
    useEffect(()=>{
      var publicService= new PublicTiltsService();
      var x= publicService.getUrls().subscribe(data=>{
        data.forEach( it=>{
            publicService.nrTilts(it).subscribe(a=> 
            {
              console.log("obtaining ", a );            
              addTilts((prevState:publicTilt[])=>  [...prevState, a] );
            }
            );
          });
  
      });
      return ()=> x.unsubscribe();
    },[]);//very important!


    if (publicTilts.length === 0) {
        return <> { <div>Loading</div> } </>;
    }

    return <>{
    <div>
    <h2>Public URLS {publicTilts.length} </h2>
    {publicTilts.map((tilt) => 
        (
          <div key={tilt.url}>
        <a href={`${tilt.url}`}>{tilt.url}</a>     {tilt.nrTilts}    
        </div>
        )
    )
    }
    
  </div>
    }</>
}

export default PublicTilts;
