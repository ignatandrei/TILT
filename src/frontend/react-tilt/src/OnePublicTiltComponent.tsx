import React, { useEffect,  useState } from 'react';
import { useParams } from 'react-router-dom';
import Stack from '@mui/material/Stack';
import './App.css';
import { TILT } from './classes/TILT';
import { PublicTiltsService } from './services/public-tilts.service';
import { Button } from '@mui/material';

function OnePublicTiltComponent() : JSX.Element{

    let { id } = useParams();
    const [tilts, addTilts] = useState(Array<TILT>(0));
    const [maxTilts,setMaxTilts]=useState(0);
    const copyItem = (tilt:TILT) => {
        window.alert(tilt.text);
        return undefined;
      }
    
      useEffect(() => {
        document.title = "TILTS for "+id;
      }, []);    
    useEffect(()=>{
        var publicService= new PublicTiltsService();
         var x= publicService.nrTilts(id||'').subscribe(a=> 
              {
                console.log("obtaining ", a );            
                setMaxTilts(()=> a?.nrTilts||0);
              }
              );
        return ()=> x.unsubscribe();      
    },[]);//very important!
  
    useEffect(()=>{
        var publicService= new PublicTiltsService();
        var x= publicService.getTilts(id||'',100000).subscribe(data=>{
            addTilts(data);
        });
        return ()=>x.unsubscribe();
      },[]);
  
    return <>
    <h1>Tilts for  {id}  {tilts.length} / {maxTilts} </h1>

    <table border={1}>
        <tr>
          <th>Nr</th>
          <th>Date</th>          
          <th>TILT</th>
          <th>Actions</th>
        </tr>
      
    {tilts.map((item,index) => 
        (
          <tr key={item.id}>
                
         <td>{maxTilts - index >0 ?  maxTilts - index : ""}  </td>
          <td>{item.LocalDateString} </td>
          <td>{item.text} <a href="{item.link" target="_blank">{item?.link}</a> </td>
            <td><Button  color="success" component="label" variant="contained" onClick={()=>copyItem(item)} >Copy</Button></td>
        </tr>
        )
    )
    }
    </table>
    
    </>

}

export default OnePublicTiltComponent;

