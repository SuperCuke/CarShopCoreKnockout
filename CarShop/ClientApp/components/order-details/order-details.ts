import * as ko from 'knockout';
import { Route, Router } from '../../router';
import * as httpClient from '../../services/HttpClient';

class OrderDetailsViewModel {
    public OrderData: IOrderData;
    public IsDataReady: KnockoutObservable<boolean>;

    constructor(params: any) {
        this.IsDataReady = ko.observable(false);
        console.log(params);
        this.initialize(params.id, params["?query"].secureCode);
    }

    async initialize(id: number, secureCode: string) {
        this.OrderData = await httpClient.get<IOrderData>('/api/carOrders/' + id + '/?secureCode=' + secureCode);
        this.IsDataReady(true);
    }
}

export default { viewModel: OrderDetailsViewModel, template: require('./order-details.html') };
