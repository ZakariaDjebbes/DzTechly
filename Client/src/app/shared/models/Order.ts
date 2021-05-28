import { IAddress } from "./IAddress";
import { IPersonalInformation } from "./IPersonalInformation";

export interface IOrderToCreate {
    cartId: string;
    deliveryMethodId: number;
    shippingAddress: IAddress;
    personalInformation: IPersonalInformation;
}

export interface IOrder {
    id: number;
    buyerEmail: string;
    orderDate: string;
    shipToAddress: IAddress;
    personalInformation: IPersonalInformation;
    deliveryMethod: string;
    shippingPrice: number;
    orderItems: IOrderItem[];
    subtotal: number;
    status: string;
    total: number;
}

export interface IOrderItem {
    productId: number;
    productName: string;
    price: number;
    quantity: number;
    pictureUrl: string;
}
