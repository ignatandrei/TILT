import React, { useEffect, useState } from 'react';
import './App.css';
import { publicTilt } from './classes/publicTilt';
interface PublicTiltsToDisplay {
  displayTilts : Array<publicTilt>;
}
export default function PublicTiltsContainer (displayTilts: PublicTiltsToDisplay)  
{
    
    const publicTilts =displayTilts.displayTilts;
    useEffect(() => {
        console.log("hello");
        return () => console.log("goodbye");
      }, []);
      


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

