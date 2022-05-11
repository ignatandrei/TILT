import { AfterContentInit, AfterViewInit, Component } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { ShepherdService } from 'angular-shepherd';

@Component({
  selector: 'app-tilt-main',
  templateUrl: './tilt-main.component.html',
  styleUrls: ['./tilt-main.component.css'],
})
export class TiltMainComponent implements AfterContentInit {
  isHandset$: Observable<boolean> = this.breakpointObserver
    .observe(Breakpoints.Handset)
    .pipe(
      map((result) => result.matches),
      shareReplay()
    );

  constructor(
    private breakpointObserver: BreakpointObserver,
    private shepherdService: ShepherdService
  ) {}
  ngAfterContentInit(): void {
    
    console.log(document.getElementsByClassName('publicUrls'));
    // this.shepherdService.requiredElements = [
    //   {
    //     selector: '.publicUrls',
    //     message: 'No search results found. Please execute another search, and try to start the tour again.',
    //     title: 'No results'
    //   }
    // ];
    this.shepherdService.defaultStepOptions = {
      classes: 'custom-class-name-1 custom-class-name-2',
      scrollTo: false,
      cancelIcon: {
        enabled: true,
      },
      highlightClass: 'highlight',

      buttons: [
        {
          classes: 'shepherd-button-secondary',
          text: 'Exit',
          action(){
            return this.cancel();
          }
        },
        {
          classes: 'shepherd-button-primary',
          text: 'Back',
          action() {
            return this.back();
          },
        },
        {
          classes: 'shepherd-button-primary',
          text: 'Next',
          action() {
            return this.next();
          },
        },
      ],
    };
    this.shepherdService.modal = true;
    this.shepherdService.confirmCancel = false;

    this.shepherdService.addSteps([
      {
        id: 'intro',
        title: 'TILT!',
        text: ['Welcome to Thing I Learnt  Today  =  TILT'],
      },
      {
        id: 'publicUrls',
        attachTo: {
          element: '.publicUrls',
          on: 'right',
        },
        title: 'TILT!',
        text: ['here you can find all public urls'],
      },
      {
        id: 'publicUrl',
        attachTo: {
          element: 'a[name="ignatandrei"]',
          on: 'right',
        },
        title: 'TILT!',
        text: ['Click on one- mine is ignatandrei'],
      }
    ]);
    this.shepherdService.start();
  }
}
