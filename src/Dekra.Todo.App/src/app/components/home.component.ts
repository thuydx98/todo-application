import { Component } from '@angular/core';
import { WorkItem } from '../models';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  selectedWorkItem?: WorkItem;

  constructor(public auth: AuthService) {}

  setSelectedWorkItem(workItem: WorkItem) {
    console.log(workItem);
    this.selectedWorkItem = Object.assign({}, workItem);
  }

  logout(): void {
    this.auth.logout();
  }
}
