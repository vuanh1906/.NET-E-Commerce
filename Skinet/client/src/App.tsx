import './App.css';

const products = [
  {name: 'product1', price: 100.00},
  {name: 'product2', price: 200.00},
  {name: 'product3', price: 300.00},
  {name: 'product4', price: 400.00},
]

function App() {
  return (
    <div>
       <h1>Vu Anh</h1>
       <ul>
          {products.map(item => (
            <li>{item.name} - {item.price}</li>
          ))}
       </ul>
    </div>
  );
}

export default App;
