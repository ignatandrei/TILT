import { ErrorHandler, Injectable } from '@angular/core';
import { MyMonitoringService } from './logging.service';
//https://devblogs.microsoft.com/premier-developer/angular-how-to-add-application-insights-to-an-angular-spa/


@Injectable()
export class ErrorHandlerService extends ErrorHandler {

    constructor(private myMonitoringService: MyMonitoringService) {
        super();
    }

    override handleError(error: Error) {
        this.myMonitoringService.logException(error); // Manually log exception
    }
}
