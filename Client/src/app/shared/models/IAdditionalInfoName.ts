export interface IAdditionalInfoName {
    name: string;
    unit?: string;
    additionalInfoCategoryName: string;
    productTypeId: number;
    additionalInfoCategoryId?: number;
    additionalInfoNameId?: number;
}

export interface IAdditionalInfoValue {
    nameId?: number;
    value: string;
    unit?: string;
    name: string;
    categoryId?: number;
    category: string;
}