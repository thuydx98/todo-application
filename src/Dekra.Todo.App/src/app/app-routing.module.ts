import { NgModule } from '@angular/core';
import { RouterModule, Routes, mapToCanActivate } from '@angular/router';
import { AuthGuard } from '@auth0/auth0-angular';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home.component';

const routes: Routes = [
  {
    path: 'work-items',
    canActivate: mapToCanActivate([AuthGuard]),
    component: HomeComponent,
  },
  {
    path: '**',
    redirectTo: 'work-items',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
