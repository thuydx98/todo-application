import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthHttpInterceptor, AuthModule } from '@auth0/auth0-angular';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { environment } from '../environments/environment';
import { HomeComponent } from './components/home.component';
import { CreateUpdateWorkItemComponent } from './components/create-update-work-item/create-update-work-item.component';
import { WorkItemListComponent } from './components/work-item-list/work-item-list.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { StoreModule } from '@ngrx/store';
import { workItemReducer } from './store/reducers/work-item.reducer';
import { WorkItemEffects } from './store/effects/work-item.effects';
import { EffectsModule } from '@ngrx/effects';

@NgModule({
  declarations: [AppComponent, HomeComponent, CreateUpdateWorkItemComponent, WorkItemListComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AuthModule.forRoot({
      domain: 'cgr.au.auth0.com',
      clientId: 'm9CS3aOPNOaST7aU4RfNXFLEFBpUXUMB',
      authorizationParams: {
        redirect_uri: window.location.origin,
        audience: 'dekra.todo.api',
        scope: 'openid profile email offline_access',
      },
      httpInterceptor: {
        allowedList: [environment.apiUrl + '/*'],
      },
    }),
    StoreModule.forRoot({ workItem: workItemReducer }),
    EffectsModule.forRoot([WorkItemEffects]),
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthHttpInterceptor, multi: true }],
  bootstrap: [AppComponent],
})
export class AppModule {}
