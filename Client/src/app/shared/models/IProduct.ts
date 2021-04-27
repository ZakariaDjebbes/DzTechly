export interface IProductAddtionalInfo {
    additionalInfoCategory: string;
    additionalInfoName: string;
    unit: string;
    additionalInfoValue: string;
}

export interface ITechnicalSheet {
    productAddtionalInfos: IProductAddtionalInfo[];
    lastUpdateDate: Date;
    referenceDate: Date;
}

export interface IWaitingCustomer {
    userName: string;
    email: string;
}

export interface IProduct {
    id: number;
    name: string;
    description: string;
    price: number;
    pictureUrl: string;
    productType: string;
    productCategory: string;
    quantity: number;
    waitingCustomersCount: number;
    isInStock: boolean;
    reviewsAverage: number;
    reviewsNumber: number;
    technicalSheet: ITechnicalSheet;
    waitingCustomers: IWaitingCustomer[];
}