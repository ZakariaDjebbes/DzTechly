export class ShopParams {
    categoryId = 0;
    typeId = 0;
    sort = 'name';
    pageNumber = 1;
    pageSize = 6;
    search: string;
    minPrice = 0.0;
    maxPrice = 0.0;
    inStock = false;
}

export class ReviewParams {
    pageNumber = 1;
    pageSize = 5;
}

export class OrderParams {
    pageNumber = 1;
    pageSize = 5;
    sort = 'dateDesc';
}

export class UsersParams {
    pageIndex = 1;
    pageSize = 5;
}