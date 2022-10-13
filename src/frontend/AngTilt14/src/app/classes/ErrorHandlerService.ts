import { ErrorHandler, Injectable } from '@angular/core';
import { MyMonitoringService } from './logging.service';
//https://devblogs.microsoft.com/premier-developer/angular-how-to-add-application-insights-to-an-angular-spa/
//https://learn.microsoft.com/en-us/azure/azure-monitor/app/javascript-angular-plugin

@Injectable()
export class ErrorHandlerService extends ErrorHandler {

    constructor(private myMonitoringService: MyMonitoringService) {
        super();
    }

    override handleError(error: Error) {
        this.myMonitoringService.logException(error); // Manually log exception
    }
}
