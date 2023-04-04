export interface Product {
    id: number;
    name: string;
    description: string;
    price: number;
    pictureUrl: string;
    productType: string;
    productBrand: string;
}

export interface ProductParams {
    sort: string;
    search?: string;
    types?: string[],
    brands?: string[];
    pageIndex: number;
    pageSize: number;
}
