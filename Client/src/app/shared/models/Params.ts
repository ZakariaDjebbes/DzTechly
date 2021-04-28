export class ShopParams{
    categoryId = 0;
    typeId = 0;
    sort = 'name';
    pageNumber = 1;
    pageSize = 6;
    search: string;
    minPrice = 0.0;
    maxPrice = 0.0;
}

export class ReviewParams {
    pageNumber = 1;
    pageSize = 5;
}