import { Component } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { PitchListComponent } from '../pitch-list/pitch-list.component';
import { PitchesActionMenuComponent } from '../pitches-action-menu/pitches-action-menu.component';
import { TranslocoModule } from '@ngneat/transloco';

@Component({
    selector: 'app-pitches-page',
    templateUrl: './pitches-page.component.html',
    styleUrls: ['./pitches-page.component.scss'],
    imports: [
    TranslocoModule,
    PitchesActionMenuComponent,
    PitchListComponent,
    AsyncPipe
]
})
export class PitchesPageComponent {}
