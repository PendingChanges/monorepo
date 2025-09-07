import { Component } from '@angular/core';
import {
  RouterLink,
  RouterLinkActive,
  RouterOutlet,
} from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
    imports: [
        
        NavbarComponent,
        RouterLink,
        RouterLinkActive,
        RouterOutlet,
    ]
})
export class AppComponent {
  title = 'journalist-crm';
}
