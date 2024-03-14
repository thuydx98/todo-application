import { Injectable } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { Observable, take, tap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthGuard {
  constructor(private auth: AuthService) {}

  canActivate() {
    return this.checkAuthentication();
  }

  private checkAuthentication(): Observable<boolean> {
    return this.auth.isAuthenticated$.pipe(
      take(1),
      tap((isAuthenticated) => {
        if (!isAuthenticated) {
          this.auth.loginWithRedirect();
        }
      })
    );
  }
}
