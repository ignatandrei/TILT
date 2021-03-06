import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginUrlGuard } from './login-url.guard';
import { LoginUrlComponent } from './login-url/login-url.component';
import { MyTiltComponent } from './my-tilt/my-tilt.component';
import { OnePublicTiltComponent } from './one-public-tilt/one-public-tilt.component';
import { PublicTiltsComponent } from './public-tilts/public-tilts.component';
import { TiltMainComponent } from './tilt-main/tilt-main.component';

const routes: Routes = [

  { path: 'tilt/public', component: PublicTiltsComponent },
  { path: 'tilt/public/:id', component: OnePublicTiltComponent },
  { path: '', redirectTo: '/tilt/public', pathMatch: 'full' },
  { path: 'tilt/my', component:MyTiltComponent , canActivate: [LoginUrlGuard] },
  { path:'loginURL', component: LoginUrlComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
