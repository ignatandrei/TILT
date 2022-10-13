import { Component } from '@angular/core';
import { MyMonitoringService } from './classes/logging.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'AngTilt';
  constructor(private myMonitoringService: MyMonitoringService) {
      myMonitoringService.logEvent("startTILTapplication");    
  }
}
