import * as ko from 'knockout';
import * as httpClient from '../../services/HttpClient';

class HomePageViewModel {
    public VisibleCars: KnockoutComputed<ICarModel[]>;
    public AllCompanies: KnockoutObservableArray<ICarCompany>;
    public SelectedCompany: KnockoutObservable<ICarCompany>;
    private _allCars: KnockoutObservableArray<ICarModel>;

    constructor() {

        this.AllCompanies = ko.observableArray([]);
        this._allCars = ko.observableArray([]);
        this.SelectedCompany = ko.observable(<any>null);

        this.VisibleCars = ko.computed(() => {
            if (!this.SelectedCompany())
                return this._allCars();
            else
                return this._allCars().filter(c => c.carCompany.id == this.SelectedCompany().id);
        });

        this.initialize();
    }

    async initialize() {
        this._allCars(await httpClient.get<ICarModel[]>('/api/cars'));
        this.AllCompanies(await httpClient.get<ICarCompany[]>('/api/CarCompanies'));
    }
}

export default { viewModel: HomePageViewModel, template: require('./home-page.html') };
