import { IAdditionalInfoValue } from "./IAdditionalInfoName";

export interface IProductToCreate {
    id?: number;
    name: string;
    description: string;
    pictureUrl: string;
    quantity: number;
    price: number;
    productTypeId: number;
    productCategoryId: number;
    additionalInformations: IAdditionalInfoValue[];
}