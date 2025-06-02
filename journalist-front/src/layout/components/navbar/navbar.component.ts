import { Component } from '@angular/core';
import { LanguagePickerComponent } from '../language-picker/language-picker.component';
import { ProfileComponent } from '../profile/profile.component';

@Component({
    selector: 'app-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss'],
    imports: [LanguagePickerComponent, ProfileComponent]
})
export class NavbarComponent {}
