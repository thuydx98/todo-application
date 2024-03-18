import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  user$?: Observable<any>;
  constructor(private auth: AuthService) {}

  ngOnInit(): void {
    this.user$ = this.auth.user$;
  }

  logout(): void {
    this.auth.logout();
  }
}
