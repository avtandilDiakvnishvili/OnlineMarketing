// src/App.js
import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Header from './components/Header';
import Distributors from './components/Distributors';
import Products from './components/Products';
import Sales from './components/Sales';
import Report from './components/Report';


function App() {
  return (
    <Router>
      <div>
        <Header />
        <Routes>
          <Route path="/distributors" element={<Distributors />} />
          <Route path="/products" element={<Products />} />
          <Route path="/sales" element={<Sales />} />
          <Route path="/reports" element={<Report />} />

        </Routes>
      </div>
    </Router>
  );
}

export default App;
