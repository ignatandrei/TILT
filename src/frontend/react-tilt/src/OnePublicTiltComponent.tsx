import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import './App.css';
import { PublicTiltsService } from './services/public-tilts.service';

function OnePublicTiltComponent() : JSX.Element{

    let { id } = useParams<"id">();
    
    return <>This is a public tilt {id}</>

}

export default OnePublicTiltComponent;
