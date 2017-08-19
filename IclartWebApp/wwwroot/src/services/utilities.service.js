var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable } from '@angular/core';
var UtilitiesService = (function () {
    function UtilitiesService() {
    }
    UtilitiesService.prototype.formatDate = function (date) {
        var day = date.getDate();
        var month = date.getMonth() + 1;
        var year = date.getFullYear();
        var monthString = "";
        var dayString = "";
        if (month < 10) {
            monthString = "0" + month.toString();
        }
        else {
            monthString = month.toString();
        }
        if (day < 10) {
            dayString = "0" + day.toString();
        }
        else {
            dayString = day.toString();
        }
        return year + "-" + monthString + "-" + dayString;
    };
    UtilitiesService.prototype.convertJsonDate = function (date) {
        var dateString = date.substr(6);
        var currentTime = new Date(parseInt(dateString));
        var month = currentTime.getMonth() + 1;
        var monthString = "";
        if (month < 10) {
            monthString = "0" + month.toString();
        }
        else {
            monthString = month.toString();
        }
        var day = currentTime.getDate();
        var dayString = "";
        if (day < 10) {
            dayString = "0" + day.toString();
        }
        else {
            dayString = day.toString();
        }
        var year = currentTime.getFullYear();
        var date = year + "-" + monthString + "-" + dayString;
        return date;
    };
    return UtilitiesService;
}());
UtilitiesService = __decorate([
    Injectable(),
    __metadata("design:paramtypes", [])
], UtilitiesService);
export { UtilitiesService };
//# sourceMappingURL=utilities.service.js.map