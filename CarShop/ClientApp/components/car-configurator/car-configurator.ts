import * as ko from 'knockout';
import { Route, Router } from '../../router';
import * as httpClient from '../../services/HttpClient';

class CarConfiguratorViewModel {
    public CarModel: KnockoutObservable<ICarModel>;

    constructor(params: any) {
        this.CarModel = ko.observable(<any>null)
        this.initialize(params.id);
    }

    async initialize(id: number) {
        this.CarModel(await httpClient.get<ICarModel>('/api/cars/' + id ));
    }
} 

export default { viewModel: CarConfiguratorViewModel, template: require('./car-configurator.html') };
