import React, { useEffect } from 'react';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import useMediaQuery from '@mui/material/useMediaQuery';

// import logo from './logo.svg';
//        {/* <img src={logo} className="App-logo" alt="logo" /> */}       
import './App.css';

import PublicTilts from './public-tilts';
import { useTheme } from '@mui/material';
import MenuLeft from './menu';
function App() {

  //const isMobile = useMediaQuery('(min-width:600px)');
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down("md"));
  return <>        
        <Box sx={{ flexGrow: 1 }}>
        {/* <span>{isMobile ? <>(
          mobil
        )</>: <>(not mobil)</>
        }</span> */}

        <AppBar position="static">
          <Toolbar>          
            <IconButton
              size="large"
              edge="start"
              color="inherit"
              aria-label="menu"
              sx={{ mr: 2 }}
            >
              <MenuIcon />              
            </IconButton>
            <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
              TILT
            </Typography>
            <Button color="inherit">Login </Button>
          </Toolbar>
        </AppBar>
      </Box>
      {isMobile? (<span style={{float:"left"}}><MenuLeft /></span>):("")}
      
      <span style={{float:"left"}}><PublicTilts /></span>
      
      
  </>;
}

export default App;
