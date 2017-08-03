var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, ViewChild, ElementRef } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ProductService } from "../../services/product.service";
import { CompetitorService } from "../../services/competitor.service";
var ProductFormComponent = (function () {
    function ProductFormComponent(_service, _adminService, elementRef) {
        this._service = _service;
        this._adminService = _adminService;
        this.elementRef = elementRef;
        this.competitorPrices = [];
        this.competitorPrice = {};
        this.resultCompetitors = {
            isError: false,
            Result: null,
            ResultList: null,
            Message: ''
        };
        this.resultCategories = {
            isError: false,
            Result: null,
            ResultList: null,
            Message: ''
        };
        this.realListCompetitors = [];
        this.editForm = {};
        this.editFormData = {};
        this.result = {
            isError: false,
            Result: null,
            ResultList: null,
            Message: ''
        };
        this.product = {
            Id: 0,
            Name: "",
            CompanyPrice: 0,
            IsDeleted: false
        };
        this.productId = 0;
        this.formErrors = {
            'name': '',
            'category': ''
        };
        this.validationMessages = {
            'name': {
                'required': 'Product Name is required.'
            },
            'category': {
                'required': 'Product Category is required.'
            }
        };
    }
    ProductFormComponent.prototype.ngAfterViewChecked = function () {
        this.formChanged();
    };
    ProductFormComponent.prototype.formChanged = function () {
        var _this = this;
        if (this.currentForm === this.productForm) {
            return;
        }
        this.productForm = this.currentForm;
        if (this.productForm) {
            this.productForm.valueChanges
                .subscribe(function (data) { return _this.onValueChanged(data); });
        }
    };
    ProductFormComponent.prototype.onValueChanged = function (data) {
        if (!this.productForm) {
            return;
        }
        var form = this.productForm.form;
        for (var field in this.formErrors) {
            this.formErrors[field] = '';
            var control = form.get(field);
            if (control && control.dirty && !control.valid) {
                var messages = this.validationMessages[field];
                for (var key in control.errors) {
                    this.formErrors[field] += messages[key] + ' ';
                }
            }
        }
    };
    ProductFormComponent.prototype.addCompetitorPrice = function () {
        this.competitorPrice = {
            "CompetitorId": this.selectedCompetitor.Id,
            "Name": this.selectedCompetitor.Name,
            "Price": this.selectedPrice,
            "Competitor": this.selectedCompetitor
        };
        this.editForm[this.selectedCompetitor.Id] = false;
        this.editFormData[this.selectedCompetitor.Id] = this.selectedPrice;
        this.removeFromCompetitorList(this.selectedCompetitor.Id);
        this.competitorPrices.push(this.competitorPrice);
        this.selectedCompetitor = null;
        this.selectedPrice = null;
    };
    ProductFormComponent.prototype.edit = function (id) {
        this.editForm[id] = true;
    };
    ProductFormComponent.prototype.updatePrice = function (id) {
        var _this = this;
        this.competitorPrices.forEach(function (item, index) {
            if (item.CompetitorId == id) {
                item.Price = _this.editFormData[id];
            }
        });
        this.editForm[id] = false;
    };
    ProductFormComponent.prototype.saveProduct = function () {
        var _this = this;
        var productForm = {
            "Product": this.product,
            "ProductCategory": this.selectedCategory,
            "CompetitorPrices": this.competitorPrices
        };
        if (this.productId > 0) {
            this._service.updateProduct(this.productId, productForm)
                .subscribe(function (product) {
                _this.result = product;
                if (_this.result.isError == false) {
                    window.location.href = _this.returnUrl;
                }
            }, function (error) { return _this.errorMessage = error; });
        }
        else {
            this._service.addProduct(productForm)
                .subscribe(function (product) {
                _this.result = product;
                if (_this.result.isError == false) {
                    window.location.href = _this.returnUrl;
                }
            }, function (error) { return _this.errorMessage = error; });
        }
    };
    ProductFormComponent.prototype.removeCompetitorPrice = function (name) {
        var _this = this;
        this.competitorPrices.forEach(function (item, index) {
            if (item.Name == name) {
                _this.realListCompetitors.forEach(function (item2, index2) {
                    if (item2.Name == name) {
                        _this.resultCompetitors.ResultList.push(item2);
                    }
                });
                _this.competitorPrices.splice(index, 1);
            }
        });
    };
    ProductFormComponent.prototype.removeFromCompetitorList = function (id) {
        var _this = this;
        this.resultCompetitors.ResultList.forEach(function (item, index) {
            if (item.Id == id) {
                _this.realListCompetitors.push(item);
                console.log(_this.realListCompetitors);
                _this.resultCompetitors.ResultList.splice(index, 1);
            }
        });
    };
    ProductFormComponent.prototype.assignCategory = function (category) {
        for (var _i = 0, _a = this.resultCategories.ResultList; _i < _a.length; _i++) {
            var entry = _a[_i];
            if (entry.Id == category.Id) {
                this.selectedCategory = entry;
            }
        }
    };
    ProductFormComponent.prototype.ngOnInit = function () {
        this.resultCompetitors = JSON.parse(this.elementRef.nativeElement.getAttribute('competitors'));
        this.resultCategories = JSON.parse(this.elementRef.nativeElement.getAttribute('categories'));
        this.returnUrl = this.elementRef.nativeElement.getAttribute('returnurl');
        this.productId = this.elementRef.nativeElement.getAttribute('productid');
        if (this.productId > 0) {
            this.result = JSON.parse(this.elementRef.nativeElement.getAttribute('product'));
            this.product = this.result.Result;
            this.assignCategory(this.result.Result.ProductCategory);
            var competitorPrices = this.result.Result.CompetitorPrices;
            for (var _i = 0, competitorPrices_1 = competitorPrices; _i < competitorPrices_1.length; _i++) {
                var entry = competitorPrices_1[_i];
                this.editForm[entry.CompetitorId] = false;
                this.editFormData[entry.CompetitorId] = entry.Price;
                var competitorPrice = {
                    "CompetitorId": entry.CompetitorId,
                    "Name": entry.Competitor.Name,
                    "Price": entry.Price,
                    "Competitor": entry.Competitor
                };
                this.competitorPrices.push(competitorPrice);
                this.removeFromCompetitorList(entry.CompetitorId);
            }
        }
    };
    return ProductFormComponent;
}());
__decorate([
    ViewChild('productForm'),
    __metadata("design:type", NgForm)
], ProductFormComponent.prototype, "currentForm", void 0);
ProductFormComponent = __decorate([
    Component({
        selector: 'product-form',
        template: require("./productForm.component.html")
    }),
    __metadata("design:paramtypes", [ProductService, CompetitorService, ElementRef])
], ProductFormComponent);
export { ProductFormComponent };
//# sourceMappingURL=productForm.component.js.map