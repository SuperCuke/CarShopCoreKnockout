import * as ko from 'knockout';
import { Route, Router } from '../../router';
import * as httpClient from '../../services/HttpClient';

class CarConfiguratorViewModel {
    public CarModel: ICarModel;
    public CarFeatures: KnockoutObservableArray<CarFeature>;
    public IsDataReady: KnockoutObservable<boolean>;
    public TotalPrice: KnockoutComputed<number>;
    public SelectedFeatures: KnockoutComputed<CarFeature[]>;
    public Email: KnockoutObservable<string> = ko.observable('').extend({
        required: true,
        email: true
    });
    public FullName: KnockoutObservable<string> = ko.observable('').extend({
        required: true
    });;
    public ValidationModel: KnockoutObservable<any>;

    public EngineOptionGroup: OptionGroup;
    public ColorOptionGroup: OptionGroup;
    public DisksOptionGroup: OptionGroup;
    public ExtraOptionGroup: OptionGroup;
    public SelectedExtraOptionsCount: KnockoutComputed<number>;

    constructor(params: any) {
        this.IsDataReady = ko.observable(false);
        this.initialize(params.id);
    } 

    async initialize(id: number) {
        var data = await httpClient.get<ICarModel>('/api/cars/' + id)
        this.CarModel = data;
        this.CarFeatures = ko.observableArray(data.supportedFeatures.map(f => new CarFeature(f)));

        this.EngineOptionGroup = new OptionGroup(CarFeatureType.EnginePerfomance, this.CarFeatures);
        this.ColorOptionGroup = new OptionGroup(CarFeatureType.Color, this.CarFeatures);
        this.DisksOptionGroup = new OptionGroup(CarFeatureType.Disks, this.CarFeatures);
        this.ExtraOptionGroup = new OptionGroup(CarFeatureType.ExtraOptions, this.CarFeatures, false);

        this.SelectedFeatures = ko.computed(() => this.CarFeatures().filter(f => f.isSelected()));

        this.TotalPrice = ko.computed(() => this.CarModel.basePrice + this.SelectedFeatures()
            .map(e => e.data.price)
            .reduce((a, b) => a + b, 0));

        this.SelectedExtraOptionsCount = ko.computed(() => this.ExtraOptionGroup.Options.filter(o => o.isSelected()).length).extend({
            max: 5
        });

        this.ValidationModel = ko.validatedObservable({
            Email: this.Email,
            FullName: this.FullName,
            selectedExtraOptionsCount: this.SelectedExtraOptionsCount
        });

        this.IsDataReady(true);
    }
} 

class OptionGroup {
    constructor(carFeatureType: CarFeatureType, carFeatures: KnockoutObservableArray<CarFeature>, onlyOne: boolean = true) {
        this.Options =  carFeatures().filter(f => f.data.carFeatureType == carFeatureType);
        if (onlyOne) {
            this.SelectedOption = ko.observable(this.Options[0]);
            this.Options[0].isSelected(true);
            this.SelectedOption.subscribe((newValue) => {
                this.Options.forEach(o => o.isSelected(false));
                if (newValue)
                    newValue.isSelected(true);
            });
        }
    }

    public Options: CarFeature[];
    public SelectedOption: KnockoutObservable<CarFeature>;
}

class CarFeature {
    constructor(data: ICarFeature) {
        this.data = data;
        this.isSelected = ko.observable(false);
    }

    public data: ICarFeature;
    public isSelected: KnockoutObservable<boolean>;
}

export default { viewModel: CarConfiguratorViewModel, template: require('./car-configurator.html') };
