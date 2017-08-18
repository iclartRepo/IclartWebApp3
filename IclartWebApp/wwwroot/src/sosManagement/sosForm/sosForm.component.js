var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, ElementRef, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { SosService } from '../../services/sos.service';
import { ClientService } from '../../services/client.service';
import { ProductService } from '../../services/product.service';
import { UtilitiesService } from '../../services/utilities.service';
var SOSFormComponent = (function () {
    function SOSFormComponent(_sosService, _clientService, _productService, _utlitiesService, elementRef) {
        this._sosService = _sosService;
        this._clientService = _clientService;
        this._productService = _productService;
        this._utlitiesService = _utlitiesService;
        this.elementRef = elementRef;
        this.result = {
            isError: false,
            Result: null,
            ResultList: null,
            Message: ''
        };
        this.resultClients = {
            isError: false,
            Result: null,
            ResultList: null,
            Message: ''
        };
        this.resultProducts = {
            isError: false,
            Result: null,
            ResultList: null,
            Message: ''
        };
        this.resultProductCategories = {
            isError: false,
            Result: null,
            ResultList: null,
            Message: ''
        };
        this.resultOperation = {
            isError: false,
            Result: null,
            ResultList: null,
            Message: ''
        };
        this.resultCustomProducts = {
            isError: false,
            Result: null,
            ResultList: null,
            Message: ''
        };
        this.editForm = {};
        this.editFormQuantity = {};
        this.editFormPrice = {};
        this.productList = [];
        this.unitOptions = [];
        this.pointerMarker = 0;
        this.selectedClient = {
            Office_Address: '',
            Combine_Items: false
        };
        this.productsView = [];
        this.customQuantity = 1;
        this.showPriceValidation = false;
        this.showNameValidation = false;
        this.sosDate = this._utlitiesService.formatDate(new Date());
        this.pickup = false;
        this.remarks = "";
        this.standardProducts = [];
        this.customProducts = [];
        this.standardFormErrors = {
            'categorySelect': '',
            'productSelect': '',
            'unitSelect': '',
            'productQuantity': '',
            'unitPrice': ''
        };
        this.standardValidationMessages = {
            'categorySelect': {
                'required': 'Product Category is required.'
            },
            'productSelect': {
                'required': 'Product is required.'
            },
            'unitSelect': {
                'required': 'Unit is required.'
            },
            'productQuantity': {
                'required': 'Quantity is required.'
            },
            'unitPrice': {
                'required': 'Unit Price is required.'
            }
        };
    }
    SOSFormComponent.prototype.ngAfterViewChecked = function () {
        this.formChanged();
    };
    SOSFormComponent.prototype.formChanged = function () {
        var _this = this;
        if (this.currentForm === this.standardProductForm) {
            return;
        }
        this.standardProductForm = this.currentForm;
        if (this.standardProductForm) {
            this.standardProductForm.valueChanges
                .subscribe(function (data) { return _this.onValueChanged(data); });
        }
    };
    SOSFormComponent.prototype.onValueChanged = function (data) {
        if (!this.standardProductForm) {
            return;
        }
        if (this.standardProductForm) {
            var form = this.standardProductForm.form;
            for (var field in this.standardFormErrors) {
                this.standardFormErrors[field] = '';
                var control = form.get(field);
                if (control && control.dirty && !control.valid) {
                    var messages = this.standardValidationMessages[field];
                    for (var key in control.errors) {
                        this.standardFormErrors[field] += messages[key] + ' ';
                    }
                }
            }
        }
    };
    SOSFormComponent.prototype.checkCustomPrice = function () {
        if (this.customPrice) {
            this.showPriceValidation = false;
        }
        else {
            this.showPriceValidation = true;
        }
    };
    SOSFormComponent.prototype.checkProductName = function () {
        if (this.customItemDescription) {
            this.showNameValidation = false;
        }
        else {
            this.showNameValidation = true;
        }
    };
    SOSFormComponent.prototype.edit = function (id) {
        this.editForm[id] = true;
    };
    SOSFormComponent.prototype.updateOrder = function (id, custom) {
        var searchResult = this.productsView.filter(function (item) { return item.Id == id; })[0];
        searchResult.Quantity = this.editFormQuantity[id];
        searchResult.Price = this.editFormPrice[id];
        searchResult.TotalPrice = searchResult.Quantity * searchResult.Price;
        if (custom == false) {
            var realProductSearch = this.standardProducts.filter(function (item) { return item.Pointer == id; })[0];
            realProductSearch.Quantity = this.editFormQuantity[id];
            realProductSearch.Price = this.editFormPrice[id];
        }
        else {
            var realProductSearch = this.customProducts.filter(function (item) { return item.Pointer == id; })[0];
            realProductSearch.Quantity = this.editFormQuantity[id];
            realProductSearch.Price = this.editFormPrice[id];
        }
        this.editForm[id] = false;
    };
    SOSFormComponent.prototype.filterPreviousCustomProducts = function () {
        var _this = this;
        var product = this.resultCustomProducts.ResultList.filter(function (i) { return i.ItemDescription == _this.selectedCustom.ItemDescription; })[0];
        this.customCategory = product.Category;
        this.customItemDescription = product.ItemDescription;
        this.customUnit = product.Unit;
        this.customQuantity = 1;
        this.customPrice = product.Price;
        this.checkCustomPrice();
        this.checkProductName();
    };
    SOSFormComponent.prototype.filterProducts = function () {
        this.productList = [];
        var categoryName = "";
        for (var _i = 0, _a = this.resultProducts.ResultList; _i < _a.length; _i++) {
            var entry = _a[_i];
            if (entry.ProductCategory.Id == this.selectedProductCategory) {
                categoryName = entry.ProductCategory.Name;
                this.productList.push(entry);
            }
        }
        if (categoryName == "Continuous Form") {
            this.selectedUnit = 'Box';
        }
        else if (categoryName == "Paper Bag") {
            this.selectedUnit = 'Bundle';
        }
        else if (categoryName == "POS Rolls") {
            this.selectedUnit = "Roll";
        }
        else {
            this.selectedUnit = "Roll";
        }
    };
    SOSFormComponent.prototype.clearStandardProductFields = function () {
        this.selectedProductCategory = null;
        this.selectedProduct = null;
        this.selectedUnit = null;
        this.productPrice = null;
        this.productQuantity = null;
        this.productList = [];
    };
    SOSFormComponent.prototype.clearCustomProductFields = function () {
        this.customItemDescription = null;
        this.customCategory = null;
        this.customPrice = null;
        this.customQuantity = 1;
        this.customUnit = null;
        this.getCustomProducts(this.selectedClient.Id);
    };
    SOSFormComponent.prototype.getCustomProducts = function (clientId) {
        var _this = this;
        this._sosService.getCustomProducts(clientId)
            .subscribe(function (customProducts) {
            _this.resultCustomProducts = customProducts;
        }, function (error) { return _this.errorMessage = error; });
    };
    SOSFormComponent.prototype.getListClients = function () {
        var _this = this;
        this._clientService.getClients()
            .subscribe(function (peoples) { return _this.resultClients = peoples; }, function (error) { return _this.errorMessage = error; });
    };
    SOSFormComponent.prototype.getProducts = function () {
        var _this = this;
        this._productService.getProducts()
            .subscribe(function (products) {
            _this.resultProducts = products;
            for (var _i = 0, _a = _this.resultProducts.ResultList; _i < _a.length; _i++) {
                var entry = _a[_i];
                _this.productList.push(entry);
            }
        }, function (error) { return _this.errorMessage = error; });
    };
    SOSFormComponent.prototype.getProductCategories = function () {
        var _this = this;
        this._productService.getProductCategories()
            .subscribe(function (categories) {
            _this.resultProductCategories = categories;
        }, function (error) { return _this.errorMessage = error; });
    };
    SOSFormComponent.prototype.initializeUnits = function () {
        this.unitOptions.push("Box");
        this.unitOptions.push("Roll");
        this.unitOptions.push("Bundle");
    };
    SOSFormComponent.prototype.onBack = function () {
        window.location.href = this.returnUrl;
    };
    SOSFormComponent.prototype.getBestStandardPrice = function () {
        var _this = this;
        this._productService.getPrice(this.selectedClient.Id, this.selectedProduct.Id)
            .subscribe(function (result) {
            _this.productPrice = result;
            _this.productQuantity = 1;
        }, function (error) { return _this.errorMessage = error; });
    };
    SOSFormComponent.prototype.addStandardProduct = function () {
        this.pointerMarker += 1;
        var standardProduct = {
            "Pointer": this.pointerMarker,
            "ProductId": this.selectedProduct.Id,
            "Quantity": this.productQuantity,
            "Price": this.productPrice,
            "Unit": this.selectedUnit
        };
        this.standardProducts.push(standardProduct);
        var productView = {
            "Id": this.pointerMarker,
            "Quantity": this.productQuantity,
            "ItemDescription": this.selectedProduct.Name,
            "Price": this.productPrice,
            "TotalPrice": this.productQuantity * this.productPrice,
            "Unit": this.selectedUnit,
            "Custom": false
        };
        this.editForm[this.pointerMarker] = false;
        this.editFormPrice[this.pointerMarker] = this.productPrice;
        this.editFormQuantity[this.pointerMarker] = this.productQuantity;
        this.productsView.push(productView);
    };
    SOSFormComponent.prototype.addCustomProduct = function () {
        this.pointerMarker += 1;
        var customProduct = {
            "Pointer": this.pointerMarker,
            "Category": this.customCategory,
            "Quantity": this.customQuantity,
            "Price": this.customPrice,
            "Unit": this.customUnit,
            "ItemDescription": this.customItemDescription
        };
        this.customProducts.push(customProduct);
        var productView = {
            "Id": this.pointerMarker,
            "Quantity": this.customQuantity,
            "ItemDescription": this.customItemDescription,
            "Price": this.customPrice,
            "TotalPrice": this.customPrice * this.customQuantity,
            "Unit": this.customUnit,
            "Custom": true
        };
        this.editForm[this.pointerMarker] = false;
        this.editFormPrice[this.pointerMarker] = this.customPrice;
        this.editFormQuantity[this.pointerMarker] = this.customQuantity;
        this.productsView.push(productView);
    };
    SOSFormComponent.prototype.removeProduct = function (id, custom) {
        this.productsView = this.productsView.filter(function (item) { return item.Id !== id; });
        if (custom == false) {
            this.standardProducts = this.standardProducts.filter(function (item) { return item.Pointer != id; });
        }
        else {
            this.standardProducts = this.customProducts.filter(function (item) { return item.Pointer != id; });
        }
    };
    SOSFormComponent.prototype.saveSos = function () {
        var _this = this;
        var sosModel = {
            "ClientId": this.selectedClient.Id,
            "Sos_Date": this.sosDate,
            "Remarks": this.remarks,
            "Status": false,
            "Pickup": this.pickup,
            "Exported": false
        };
        var dataModel = {
            "Sos": sosModel,
            "StandardOrders": this.standardProducts,
            "CustomOrders": this.customProducts
        };
        this._sosService.addSos(dataModel)
            .subscribe(function (result) {
            _this.resultOperation = result;
            if (_this.resultOperation.isError == false) {
                window.location.href = _this.returnUrl;
            }
        }, function (error) { return _this.errorMessage = error; });
    };
    SOSFormComponent.prototype.ngOnInit = function () {
        this.initializeUnits();
        this.resultClients = JSON.parse(this.elementRef.nativeElement.getAttribute('clients'));
        this.resultProducts = JSON.parse(this.elementRef.nativeElement.getAttribute('products'));
        this.resultProductCategories = JSON.parse(this.elementRef.nativeElement.getAttribute('productcategories'));
        this.returnUrl = this.elementRef.nativeElement.getAttribute('returnurl');
        this.selectedClient.Office_Address = "";
    };
    return SOSFormComponent;
}());
__decorate([
    ViewChild('standardProductForm'),
    __metadata("design:type", NgForm)
], SOSFormComponent.prototype, "currentForm", void 0);
SOSFormComponent = __decorate([
    Component({
        selector: 'sos-form',
        template: require('./sosForm.component.html')
    }),
    __metadata("design:paramtypes", [SosService, ClientService, ProductService, UtilitiesService, ElementRef])
], SOSFormComponent);
export { SOSFormComponent };
//# sourceMappingURL=sosForm.component.js.map