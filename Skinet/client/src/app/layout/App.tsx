import { ThemeProvider } from '@emotion/react';
import { Container, createTheme, CssBaseline } from '@mui/material';
import { useEffect, useState } from 'react';
import { Outlet } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import Header from './Header';
import 'react-toastify/dist/ReactToastify.css'
import { useStoreContext } from '../context/StoreContext';
import { getCookie } from '../util/util';
import agent from '../api/agent';
import LoadingComponent from './LoadingComponent';

function App() {
  const {setBasket} = useStoreContext();
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const buyerId = getCookie('buyerId');
    console.log('hihi')
    console.log(buyerId)
    if (buyerId) {
      agent.Basket.get()
        .then(basket => setBasket(basket))
        .catch(error => console.log(error))
        .finally(() => setLoading(false));
    } else {
      setLoading(false);
    }
  }, [setBasket])

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
  
   if(loading) return <LoadingComponent message='Init app'/>

  return (
    <>
      <ThemeProvider theme={theme}>
        <ToastContainer position='bottom-right' hideProgressBar theme='colored'/>
        <CssBaseline />
        <Header handleThemeChange={handleThemeChange} darkMode={darkMode}/>
        <Container>
          <Outlet />
        </Container>
      </ThemeProvider>

    </>
  );
}

export default App;
