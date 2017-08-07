import { Component, OnInit, ElementRef } from '@angular/core';

import { IMessageResult } from '../../interfaces/messageResult.interface';

import { SosService } from '../../services/sos.service';
@Component({
    selector: 'sos-list',
    template: require("./sosList.component.html")
})
export class SOSListComponent {

    constructor(private _sosService: SosService, private elementRef: ElementRef) { }

    result: IMessageResult = {
        isError: false,
        Result: null,
        ResultList: null,
        Message: ''
    };
    errorMessage: string;

    /* Initializer and Native Functions */
    ngOnInit(): void {
        this.result = JSON.parse(this.elementRef.nativeElement.getAttribute('sos'));
    }
}
