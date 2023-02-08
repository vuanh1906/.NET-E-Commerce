import { useState, useEffect } from "react";
import { Product } from "../../app/models/product";
import ProductList from "./ProductList";


export default function Catalog() {
    const [products, setProducts] = useState<Product[]>([]);

    useEffect(() => {
        fetch('https://localhost:5001/api/products?pagesize=10')
            .then(response => response.json())
            .then(data => setProducts(data.data))

    }, [])


    return (
        <>
            <ProductList products={products} />        
        </>
    )
}