import { Component } from '@angular/core';
import { AsyncPipe, CommonModule } from '@angular/common';
import { ClientListComponent } from '../client-list/client-list.component';
import { ClientsActionMenuComponent } from '../clients-action-menu/clients-action-menu.component';
import { TranslocoModule } from '@ngneat/transloco';

@Component({
    selector: 'app-clients-page',
    templateUrl: './clients-page.component.html',
    styleUrls: ['./clients-page.component.scss'],
    imports: [
        TranslocoModule,
        CommonModule,
        ClientsActionMenuComponent,
        ClientListComponent,
        AsyncPipe,
    ]
})
export class ClientsComponent {}
