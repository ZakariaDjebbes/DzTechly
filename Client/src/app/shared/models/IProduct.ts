export interface IProduct {
    id: number
    name: string
    description: string
    price: number
    pictureUrl: string
    productType: string
    productCategory: string
    quantity: number
    waitingCustomersCount: number
    isInStock: boolean
    reviewsAverage: number
    reviewsNumber: number
    technicalSheet: ITechnicalSheet
    waitingCustomers: IWaitingCustomer[]
  }
  
  export interface ITechnicalSheet {
    productAddtionalInfos: {[key:string]:IProductInfo}
    lastUpdateDate: string
    referenceDate: string
  }
  
  export interface IWaitingCustomer {
    userName: string
    email: string
  }
  
  export interface IProductInfo {
    additionalInfoName: string,
    unit: string,
    additionalInfoValue: string
  }