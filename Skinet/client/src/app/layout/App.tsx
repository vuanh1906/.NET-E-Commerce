import { ThemeProvider } from '@emotion/react';
import { CssBaseline } from '@mui/material';
import { createTheme } from '@mui/material/styles';
import { Container } from '@mui/system';
import { useState } from 'react';
import Catalog from '../../features/catalog/Catalog';
import Header from './Header';
function App() {
  const [darkMode, setDarkMode] = useState(false);

  function handleThemeChange() {
    setDarkMode(!darkMode);
    
  }
  const paletteType = darkMode ? 'dark' : 'light';
  
  const theme = createTheme({
    palette: {
      mode: paletteType,
      background: {
        default: paletteType ==='light' ? '#eaeaea' : '#121212'
      }
    }
  })
  

  return (
    <>
      <ThemeProvider theme={theme}>
        
        <CssBaseline />
        <Header handleThemeChange={handleThemeChange} darkMode={darkMode}/>
        <Container>
          <Catalog />
        </Container>
      </ThemeProvider>

    </>
  );
}

export default App;
