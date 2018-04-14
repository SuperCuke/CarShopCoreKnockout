webpackJsonp([1],{

/***/ 37:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0_knockout__ = __webpack_require__(1);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0_knockout___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_0_knockout__);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__services_HttpClient__ = __webpack_require__(42);
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = y[op[0] & 2 ? "return" : op[0] ? "throw" : "next"]) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [0, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};


var CarConfiguratorViewModel = (function () {
    function CarConfiguratorViewModel(params) {
        this.Email = __WEBPACK_IMPORTED_MODULE_0_knockout__["observable"]('').extend({
            required: true
        });
        this.Name = __WEBPACK_IMPORTED_MODULE_0_knockout__["observable"]('').extend({
            required: true
        });
        this.IsDataReady = __WEBPACK_IMPORTED_MODULE_0_knockout__["observable"](false);
        this.initialize(params.id);
    }
    ;
    CarConfiguratorViewModel.prototype.initialize = function (id) {
        return __awaiter(this, void 0, void 0, function () {
            var _this = this;
            var data;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, __WEBPACK_IMPORTED_MODULE_1__services_HttpClient__["a" /* get */]('/api/cars/' + id)];
                    case 1:
                        data = _a.sent();
                        this.CarModel = data;
                        this.CarFeatures = __WEBPACK_IMPORTED_MODULE_0_knockout__["observableArray"](data.supportedFeatures.map(function (f) { return new CarFeature(f); }));
                        this.EngineOptionGroup = new OptionGroup(0 /* EnginePerfomance */, this.CarFeatures);
                        this.ColorOptionGroup = new OptionGroup(3 /* Color */, this.CarFeatures);
                        this.DisksOptionGroup = new OptionGroup(1 /* Disks */, this.CarFeatures);
                        this.ExtraOptionGroup = new OptionGroup(2 /* ExtraOptions */, this.CarFeatures, false);
                        this.SelectedFeatures = __WEBPACK_IMPORTED_MODULE_0_knockout__["computed"](function () { return _this.CarFeatures().filter(function (f) { return f.isSelected(); }); });
                        this.TotalPrice = __WEBPACK_IMPORTED_MODULE_0_knockout__["computed"](function () { return _this.CarModel.basePrice + _this.SelectedFeatures()
                            .map(function (e) { return e.data.price; })
                            .reduce(function (a, b) { return a + b; }, 0); });
                        this.IsDataReady(true);
                        return [2 /*return*/];
                }
            });
        });
    };
    return CarConfiguratorViewModel;
}());
var OptionGroup = (function () {
    function OptionGroup(carFeatureType, carFeatures, onlyOne) {
        if (onlyOne === void 0) { onlyOne = true; }
        var _this = this;
        this.Options = carFeatures().filter(function (f) { return f.data.carFeatureType == carFeatureType; });
        if (onlyOne) {
            this.SelectedOption = __WEBPACK_IMPORTED_MODULE_0_knockout__["observable"](this.Options[0]);
            this.Options[0].isSelected(true);
            this.SelectedOption.subscribe(function (newValue) {
                _this.Options.forEach(function (o) { return o.isSelected(false); });
                if (newValue)
                    newValue.isSelected(true);
            });
        }
    }
    return OptionGroup;
}());
var CarFeature = (function () {
    function CarFeature(data) {
        this.data = data;
        this.isSelected = __WEBPACK_IMPORTED_MODULE_0_knockout__["observable"](false);
    }
    return CarFeature;
}());
/* harmony default export */ __webpack_exports__["default"] = ({ viewModel: CarConfiguratorViewModel, template: __webpack_require__(43) });


/***/ }),

/***/ 41:
/***/ (function(module, exports, __webpack_require__) {

module.exports = (__webpack_require__(0))(13);

/***/ }),

/***/ 42:
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (immutable) */ __webpack_exports__["a"] = get;
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0_isomorphic_fetch__ = __webpack_require__(41);
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0_isomorphic_fetch___default = __webpack_require__.n(__WEBPACK_IMPORTED_MODULE_0_isomorphic_fetch__);
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = y[op[0] & 2 ? "return" : op[0] ? "throw" : "next"]) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [0, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};

function get(url) {
    return __awaiter(this, void 0, void 0, function () {
        var response, result;
        return __generator(this, function (_a) {
            switch (_a.label) {
                case 0: return [4 /*yield*/, fetch(url)];
                case 1:
                    response = _a.sent();
                    return [4 /*yield*/, response.json()];
                case 2:
                    result = _a.sent();
                    return [2 /*return*/, result];
            }
        });
    });
}


/***/ }),

/***/ 43:
/***/ (function(module, exports) {

module.exports = "<div data-bind=\"if: IsDataReady\">\r\n    <div class=\"container marketing\">\r\n        <div data-bind=\"with: CarModel\">\r\n            <h1 data-bind=\"text: carCompany.name + ' ' + name\"></h1>\r\n            <div class=\"row featurette\">\r\n                <div class=\"col-md-7 order-md-2\">\r\n                    <p class=\"lead\" data-bind=\"text: longDescription\"></p>\r\n                </div>\r\n                <div class=\"col-md-5 order-md-1\">\r\n                    <img class=\"featurette-image img-fluid mx-auto\" style=\"max-width: 500px; max-height: 500px;\" data-bind=\"attr:{'src': '/carImages/' + imagePath}\">\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <hr>\r\n        <div class=\"row\">\r\n            <div class=\"col-md-8 order-md-1\">\r\n                <form>\r\n                    <div data-bind=\"with: EngineOptionGroup\">\r\n                        <h4>Choose desired power</h4>\r\n                        <div data-bind=\"foreach: Options\">\r\n                            <div class=\"radio\">\r\n                                <label><input type=\"radio\" name=\"engine\" data-bind=\"checkedValue: $data, checked: $parent.SelectedOption\" /><span data-bind=\"text: data.description\"></span> - <span data-bind=\"text: data.price\"></span> EUR</label>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                    <hr>\r\n                    <div data-bind=\"with: ColorOptionGroup\">\r\n                        <h4>Choose desired color</h4>\r\n                        <div data-bind=\"foreach: Options\">\r\n                            <div class=\"radio\">\r\n                                <label><input type=\"radio\" name=\"color\" data-bind=\"checkedValue: $data, checked: $parent.SelectedOption\" /><span data-bind=\"text: data.description\"></span></label>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                    <hr>\r\n                    <div data-bind=\"with: DisksOptionGroup\">\r\n                        <h4>Choose desired disks</h4>\r\n                        <div data-bind=\"foreach: Options\">\r\n                            <div class=\"radio\">\r\n                                <label><input type=\"radio\" name=\"disk\" data-bind=\"checkedValue: $data, checked: $parent.SelectedOption\" /><span data-bind=\"text: data.description\"></span> - <span data-bind=\"text: data.price\"></span> EUR</label>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                    <hr>\r\n                    <div data-bind=\"with: ExtraOptionGroup\">\r\n                        <h4>Choose extra options</h4>\r\n                        <div data-bind=\"foreach: Options\">\r\n                            <div class=\"checkbox\">\r\n                                <label>\r\n                                    <input type=\"checkbox\" value=\"\" data-bind=\"checked: isSelected\">\r\n                                    <span data-bind=\"text: data.description\"></span> - <span data-bind=\"text: data.price\"></span> EUR\r\n                                </label>\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                </form>\r\n            </div>\r\n            <div class=\"col-md-4 order-md-2 mb-4\">\r\n                <h4 class=\"d-flex justify-content-between align-items-center mb-3\">\r\n                    <span class=\"text-muted\">Your cart</span>\r\n                </h4>\r\n                <ul class=\"list-group mb-3\">\r\n                    <li class=\"list-group-item d-flex justify-content-between lh-condensed\">\r\n                        <div>\r\n                            <h6 class=\"my-0\" data-bind=\"text:  CarModel.carCompany.name + ' ' + CarModel.name + '- basic price'\"></h6>\r\n                        </div>\r\n                        <span class=\"text-muted\" data-bind=\"text: CarModel.basePrice + ' EUR'  \"></span>\r\n                    </li>\r\n                    <div data-bind=\"foreach: SelectedFeatures\">\r\n                        <li class=\"list-group-item d-flex justify-content-between lh-condensed\">\r\n                            <div>\r\n                                <h6 class=\"my-0\" data-bind=\"text: data.description\"></h6>\r\n                            </div>\r\n                            <span class=\"text-muted\" data-bind=\"text: data.price + ' EUR'\"></span>\r\n                        </li>\r\n                    </div>\r\n                    <li class=\"list-group-item d-flex justify-content-between\">\r\n                        <span>Total (EUR):</span>\r\n                        <strong data-bind=\"text: TotalPrice\"></strong>\r\n                    </li>\r\n                </ul>\r\n                <form class=\"card  p-2\">\r\n                    <div class=\"form-group\" data-bind=\"validationElement: Email\">\r\n                        <label for=\"exampleInputEmail1\">Email address</label>\r\n                        <input type=\"email\" class=\"form-control\" placeholder=\"Enter email\">\r\n                    </div>\r\n                    <div class=\"form-group\">\r\n                        <label for=\"exampleInputPassword1\">Name</label>\r\n                        <input type=\"text\" class=\"form-control\" placeholder=\"Emter full name\">\r\n                    </div>\r\n                    <div class=\"input-group\">\r\n                        <div class=\"input-group-append\">\r\n                            <button type=\"submit\" class=\"btn btn-secondary\">Place order</button>\r\n                        </div>\r\n                    </div>\r\n                </form>\r\n            </div>\r\n\r\n        </div>\r\n    </div>\r\n</div>"

/***/ })

});
//# sourceMappingURL=1.js.map