import { Injectable } from '@angular/core';

@Injectable()

export class UtilitiesService {

    constructor() {
    }

    formatDate(date: Date): string {
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

    }

}

