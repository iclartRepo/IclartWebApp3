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
import { ClientService } from "../../services/client.service";
import { CompetitorService } from "../../services/competitor.service";
var ClientFormComponent = (function () {
    function ClientFormComponent(_service, _adminService, elementRef) {
        this._service = _service;
        this._adminService = _adminService;
        this.elementRef = elementRef;
        this.competitorDiscountSchemes = [];
        this.competitorDs = {};
        this.resultCompetitors = {
            isError: false,
            Result: null,
            ResultList: null,
            Message: ''
        };
        this.editForm = {};
        this.editFormData = {};
        this.tabNum = 1;
        this.result = {
            isError: false,
            Result: null,
            ResultList: null,
            Message: ''
        };
        this.client = {
            Id: 0,
            Name: "",
            TIN: "",
            Office_Address: "",
            Warehouse_Address: "",
            Credit_Limit: 0,
            Dealer: false,
            Accounts_Receivables: 0,
            Credit_Terms: "",
            Discount_Scheme: 0,
            Agent: "",
            Contacts_Order: "",
            Contacts_Accounting: "",
            Telephone_Number: "",
            Fax_Number: "",
            Email: "",
            Rounded_Payment: false,
            Usual_Ordered_Item: "",
            Witholding_Tax: 0,
            Vat_Exemption: false,
            Collection_Period: "",
            Combine_Items: true,
            Overpayment_Counter: 0
        };
        this.formErrors = {
            'name': '',
            'telephoneNumber': '',
            'discountScheme': '',
            'contactsOrder': '',
            'creditLimit': ''
        };
        this.validationMessages = {
            'name': {
                'required': 'Client Name is required.'
            },
            'telephoneNumber': {
                'required': 'Telephone Number is required.'
            },
            'discountScheme': {
                'required': 'Discount Scheme is required.'
            },
            'contactsOrder': {
                'required': 'Contact Persons from Sales is required.'
            },
            'creditLimit': {
                'required': 'Credit Limit is required.'
            }
        };
    }
    ClientFormComponent.prototype.ngAfterViewChecked = function () {
        this.formChanged();
    };
    ClientFormComponent.prototype.formChanged = function () {
        var _this = this;
        if (this.currentForm === this.clientForm) {
            return;
        }
        this.clientForm = this.currentForm;
        if (this.clientForm) {
            this.clientForm.valueChanges
                .subscribe(function (data) { return _this.onValueChanged(data); });
        }
    };
    ClientFormComponent.prototype.onValueChanged = function (data) {
        if (!this.clientForm) {
            return;
        }
        var form = this.clientForm.form;
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
    ClientFormComponent.prototype.setTab = function (tabNumber) {
        this.tabNum = tabNumber;
    };
    ClientFormComponent.prototype.addClient = function () {
        var _this = this;
        if (this.clientId > 0) {
            var clientForm = {
                "Client": this.client,
                "CompetitorDiscountSchemes": this.competitorDiscountSchemes
            };
            this._service.updateClient(clientForm)
                .subscribe(function (client) {
                _this.result = client;
                if (_this.result.isError == false) {
                    window.location.href = _this.returnUrl;
                }
            }, function (error) { return _this.errorMessage = error; });
        }
        else {
            var clientForm = {
                "Client": this.client,
                "CompetitorDiscountSchemes": this.competitorDiscountSchemes
            };
            this._service.addClient(clientForm)
                .subscribe(function (client) {
                _this.result = client;
                if (_this.result.isError == false) {
                    window.location.href = _this.returnUrl;
                }
            }, function (error) { return _this.errorMessage = error; });
        }
    };
    ClientFormComponent.prototype.edit = function (id) {
        this.editForm[id] = true;
    };
    ClientFormComponent.prototype.addCompetitorDS = function () {
        this.competitorDs = {
            "CompetitorId": this.selectedCompetitor.Id,
            "Name": this.selectedCompetitor.Name,
            "DiscountScheme": this.selectedDs,
            "Competitor": this.selectedCompetitor
        };
        this.editForm[this.selectedCompetitor.Id] = false;
        this.editFormData[this.selectedCompetitor.Id] = this.selectedDs;
        this.removeFromCompetitorList(this.selectedCompetitor.Id);
        this.competitorDiscountSchemes.push(this.competitorDs);
        this.selectedCompetitor = null;
        this.selectedDs = null;
    };
    ClientFormComponent.prototype.updateDs = function (id) {
        var _this = this;
        this.competitorDiscountSchemes.forEach(function (item, index) {
            if (item.CompetitorId == id) {
                item.DiscountScheme = _this.editFormData[id];
            }
        });
        this.editForm[id] = false;
        console.log(this.competitorDiscountSchemes);
    };
    ClientFormComponent.prototype.removeFromCompetitorList = function (id) {
        var _this = this;
        this.resultCompetitors.ResultList.forEach(function (item, index) {
            if (item.Id == id) {
                _this.resultCompetitors.ResultList.splice(index, 1);
            }
        });
    };
    ClientFormComponent.prototype.deleteCompetitorDS = function (id, competitor) {
        var _this = this;
        this.competitorDiscountSchemes.forEach(function (item, index) {
            if (item.CompetitorId == id) {
                _this.competitorDiscountSchemes.splice(index, 1);
            }
        });
        this.resultCompetitors.ResultList.push(competitor);
    };
    ClientFormComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.returnUrl = this.elementRef.nativeElement.getAttribute('returnurl');
        this.flagUpdate = this.elementRef.nativeElement.getAttribute('flag');
        if (this.flagUpdate == "true") {
            this.result = JSON.parse(this.elementRef.nativeElement.getAttribute('client'));
            this.client = this.result.Result;
            this.clientId = this.client.Id;
        }
        else {
            this.clientId = 0;
        }
        this._adminService.getCompetitors()
            .subscribe(function (result) {
            _this.resultCompetitors = result;
            _this.realListCompetitors = result.ResultList;
            if (_this.flagUpdate == "true") {
                var dsSchemes = _this.result.Result.CompetitorDiscountSchemes;
                for (var _i = 0, dsSchemes_1 = dsSchemes; _i < dsSchemes_1.length; _i++) {
                    var entry = dsSchemes_1[_i];
                    _this.editForm[entry.CompetitorId] = false;
                    _this.editFormData[entry.CompetitorId] = entry.DiscountScheme;
                    var ds = {
                        "CompetitorId": entry.CompetitorId,
                        "Name": entry.CompetitorEntity.Name,
                        "DiscountScheme": entry.DiscountScheme,
                        "Competitor": entry.CompetitorEntity
                    };
                    _this.competitorDiscountSchemes.push(ds);
                    _this.removeFromCompetitorList(entry.CompetitorId);
                }
            }
        }, function (error) { return _this.errorMessage = error; });
    };
    ClientFormComponent.prototype.getClient = function (id) {
        var _this = this;
        this._service.getClientInfo(id).subscribe(function (result) {
            _this.result = result;
            _this.client = _this.result.Result;
            var dsSchemes = _this.result.Result.CompetitorDiscountSchemes;
            for (var _i = 0, dsSchemes_2 = dsSchemes; _i < dsSchemes_2.length; _i++) {
                var entry = dsSchemes_2[_i];
                _this.editForm[entry.CompetitorId] = false;
                _this.editFormData[entry.CompetitorId] = entry.DiscountScheme;
                var ds = {
                    "CompetitorId": entry.CompetitorId,
                    "Name": entry.CompetitorEntity.Name,
                    "DiscountScheme": entry.DiscountScheme,
                    "Competitor": entry.CompetitorEntity
                };
                _this.competitorDiscountSchemes.push(ds);
                _this.removeFromCompetitorList(entry.CompetitorId);
            }
        }, function (error) { return _this.errorMessage = error; });
    };
    return ClientFormComponent;
}());
__decorate([
    ViewChild('clientForm'),
    __metadata("design:type", NgForm)
], ClientFormComponent.prototype, "currentForm", void 0);
ClientFormComponent = __decorate([
    Component({
        selector: 'client-form',
        template: require('./clientForm.component.html')
    }),
    __metadata("design:paramtypes", [ClientService, CompetitorService, ElementRef])
], ClientFormComponent);
export { ClientFormComponent };
//# sourceMappingURL=clientForm.component.js.map