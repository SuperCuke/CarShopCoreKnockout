interface ICarModel {
    id: number;
    carCompany: ICarCompany;
    name: string;
    shortDescription: string;
    longDescription: string;
    basePrice: number;
    imagePath: string;
}

interface ICarCompany {
    id: number;
    name: string;
    description: string;
}
