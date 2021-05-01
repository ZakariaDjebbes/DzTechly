import { v4 as uuidv4 } from 'uuid';

export interface ICart {
    id: string
    items: ICartItem[]
    deliveryMethodId?: number
}

export interface ICartItem {
    id: number
    productName: string
    price: number
    quantity: number
    pictureUrl: string
    category: string
    type: string
    isInStock: boolean
}

export class Cart implements ICart {
    id = uuidv4();
    items: ICartItem[] = [];
}

export interface ICartTotals {
    shipping: number;
    total: number;
    subtotal: number;
}
