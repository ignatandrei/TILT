import { Injectable, NgModule } from '@angular/core';
import {
  RouterModule,
  RouterStateSnapshot,
  Routes,
  TitleStrategy,
} from '@angular/router';
import { AboutComponent } from './about/about.component';
import { LoginUrlGuard } from './login-url.guard';
import { LoginUrlComponent } from './login-url/login-url.component';
import { MyTiltComponent } from './my-tilt/my-tilt.component';
import { OnePublicTiltComponent } from './one-public-tilt/one-public-tilt.component';
import { PublicTiltsComponent } from './public-tilts/public-tilts.component';
import { TiltMainComponent } from './tilt-main/tilt-main.component';

const routes: Routes = [
  {
    path: 'tilt/public',
    component: PublicTiltsComponent,
    title: 'List of public tilts',
  },
  { path: 'tilt/public/:id', component: OnePublicTiltComponent },
  { path: '', redirectTo: '/tilt/public', pathMatch: 'full' },
  {
    path: 'tilt/my',
    component: MyTiltComponent,
    canActivate: [LoginUrlGuard],
    title: 'My tilts',
  },
  {
    path: 'loginURL',
    component: LoginUrlComponent,
    title: 'Login to add a tilt',
  },
  {
    path:'about',
    component: AboutComponent,
    title:'About TILT'
  }
];

@Injectable()
export class TemplatePageTitleStrategy extends TitleStrategy {
  override updateTitle(routerState: RouterStateSnapshot) {
    const title = this.buildTitle(routerState);
    if (title !== undefined) {
      document.title = `TILT! - ${title}`;
    } else {
      var arr = routerState.url.split('/');
      if(arr.length==0)
        document.title = `TILT! - AAA`;
      else
        document.title = `TILT! - tilts for ` + arr[arr.length-1];
    }
  }
}

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [{provide: TitleStrategy,  useClass: TemplatePageTitleStrategy}],
})
export class AppRoutingModule {}

