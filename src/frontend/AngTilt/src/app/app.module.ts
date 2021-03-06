import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TiltMainComponent } from './tilt-main/tilt-main.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule} from '@angular/material/tabs';
import {MatMenuModule} from '@angular/material/menu';
import { MatListModule } from '@angular/material/list';
import { MyTiltComponent } from './my-tilt/my-tilt.component';
import { PublicTiltsComponent } from './public-tilts/public-tilts.component';
import { OnePublicTiltComponent } from './one-public-tilt/one-public-tilt.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { FormatDatePipe } from 'src/app/general/pipes/formatDatePipe';
import { LoginUrlComponent } from './login-url/login-url.component';
import { BrowserStorageService} from './general/storage/browseStorage';
import { DisplayTILTComponent } from './display-tilt/display-tilt.component';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
@NgModule({
  declarations: [
    AppComponent,
    TiltMainComponent,
    MyTiltComponent,
    PublicTiltsComponent,
    OnePublicTiltComponent,
    FormatDatePipe,
    LoginUrlComponent,
    DisplayTILTComponent
  ],
  imports: [
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
    MatTabsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatMenuModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule
  ],
  providers: [
    BrowserStorageService,

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
