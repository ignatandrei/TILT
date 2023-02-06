import React, { useEffect, useState } from 'react';
import './App.css';
import { publicTilt } from './classes/publicTilt';
import PublicTiltsContainer from './public-tiltsContainer';
import { withLoading } from './reusable/WithLoading';
import { PublicTiltsService } from './services/public-tilts.service';

function PublicTilts() {
    const [publicTilts, addTilts] = useState(Array<publicTilt>(0));
    const TiltListWithLoading = withLoading(PublicTiltsContainer);
    const [isLoading, setIsLoading] = useState(true);
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
              setIsLoading(false);
              console.log("obtaining ", a );            
              addTilts((prevState:publicTilt[])=>  [...prevState, a] );
            }
            );
          });
  
      });
      return ()=> x.unsubscribe();
    },[setIsLoading]);//very important!


    // if (publicTilts.length === 0) {
    //     return <> { <div>Number tilts is still 0</div> } </>;
    // }

    return <>{
    <div>
    <TiltListWithLoading loading={isLoading} displayTilts={publicTilts}></TiltListWithLoading >
    
    
  </div>
    }</>
}

export default PublicTilts;
