import { AfterViewInit, Component } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { ShepherdService } from 'angular-shepherd';

@Component({
  selector: 'app-tilt-main',
  templateUrl: './tilt-main.component.html',
  styleUrls: ['./tilt-main.component.css']
})
export class TiltMainComponent implements AfterViewInit  {

  isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );

  constructor(private breakpointObserver: BreakpointObserver, private shepherdService: ShepherdService) {}
  ngAfterViewInit(): void {
    this.shepherdService.defaultStepOptions = {
      classes: 'custom-class-name-1 custom-class-name-2',
      scrollTo: false,
      cancelIcon: {
        enabled: true
      },
      highlightClass: 'highlight',
        
      buttons: [
        {
          classes: 'shepherd-button-secondary',
          text: 'Exit',
        },
        {
          classes: 'shepherd-button-primary',
          text: 'Back',
          action() {
            return this.back();
          }
        },
        {
          classes: 'shepherd-button-primary',
          text: 'Next',
          action() {
            return this.next();
          }
        }
        
              ]
    };;
    this.shepherdService.modal = true;
    this.shepherdService.confirmCancel = false;
    
    this.shepherdService.addSteps([
        {
          id: 'intro',
          attachTo: { 
            element: '.first-element', 
            on: 'bottom'
          },
        title: 'Welcome to TILT!',
        text: ['Things I Learned Today'],
      }
    ]);
    this.shepherdService.start();
  }

}
