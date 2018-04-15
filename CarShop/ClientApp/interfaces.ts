interface ICarModel {
    id: number;
    carCompany: ICarCompany;
    name: string;
    shortDescription: string;
    longDescription: string;
    basePrice: number;
    imagePath: string;
    supportedFeatures: ICarFeature[];
}

interface ICarCompany {
    id: number;
    name: string;
    description: string;
}

interface ICarFeature {
    id: number;
    price: number;
    name: string;
    description: string;
    carFeatureType: CarFeatureType;
}

interface IOrderData {
    id: number;
    fullName: string;
    email: string;
    carModel: ICarModel;
    carFeatures: ICarFeature[];
}

interface IRedurectModel {
    uri: string;
}

const enum CarFeatureType {
    EnginePerfomance = 0,
    Disks = 1,
    ExtraOptions = 2,
    Color = 3
}

