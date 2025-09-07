import { Component } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { PitchListComponent } from '../pitch-list/pitch-list.component';
import { PitchesActionMenuComponent } from '../pitches-action-menu/pitches-action-menu.component';

@Component({
    selector: 'app-pitches-page',
    templateUrl: './pitches-page.component.html',
    styleUrls: ['./pitches-page.component.scss'],
    imports: [
    
    PitchesActionMenuComponent,
    PitchListComponent,
    AsyncPipe
]
})
export class PitchesPageComponent {}
