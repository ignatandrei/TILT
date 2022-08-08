import { AfterContentInit, AfterViewInit, Component } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { ShepherdService } from 'angular-shepherd';
import { AmsService } from '../ams/ams.service';
import { AMSData } from '../ams/AMSData';

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
    private shepherdService: ShepherdService,
    private amsService: AmsService
  ) {}
  ams: AMSData|undefined=undefined;

  ngAfterContentInit(): void {
    this.showTutorial();    
    this.showAmsVersion();
  }
  
  showAmsVersion() {
    this.amsService.AmsDataValues().subscribe(
      it=>{
        console.log('amsService.AmsDataValues()',it);
        this.ams=it.sort((a,b)=>a!.TheDate!.getDate()-b!.TheDate!.getDate())?.pop();
      }
    )
  }
  showTutorial() {
    
    
    //console.log('showTutorial');
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
        text: ['here you can find all public TILTS'],
      },
      {
        id: 'myTilts',
        attachTo: {
          element: '.myTilts',
          on: 'right',
        },
        title: 'TILT!',
        text: ['here you can add your own TILTS'],
      },
      {
        id: 'publicUrl',
        attachTo: {
          element: 'a[name="ignatandrei"]',
          on: 'right',
        },
        showOn:function (){ return false;},
        title: 'TILT!',
        text: ['Click on one- mine is ignatandrei'],
      }
    ]);
    this.shepherdService.start();
  }
}
