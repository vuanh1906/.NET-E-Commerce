import React from 'react';
import ReactDOM from 'react-dom/client';
import './app/layout/style.css';
import reportWebVitals from './reportWebVitals';
import { RouterProvider } from 'react-router-dom';
import { router } from './app/router/Routes';
import { Provider } from 'react-redux';
import { store } from './features/store/configureStore';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

root.render(
<Provider store={store}>
      <RouterProvider router={router} />
    </Provider>
);

reportWebVitals();
