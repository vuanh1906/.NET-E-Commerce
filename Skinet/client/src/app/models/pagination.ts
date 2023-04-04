export interface MetaData {
    pageIndex: number;
    totalPage: number;
    pageSize: number;
    count: number;
}
export class PaginatedResponse<T> {
    items : T;
    metaData: MetaData;

    constructor(items: T, metaData: MetaData) {
        this.items = items;
        this.metaData = metaData;
    }
}