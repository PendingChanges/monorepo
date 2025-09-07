import { Component } from '@angular/core';
import { AsyncPipe } from '@angular/common';
import { ClientListComponent } from '../client-list/client-list.component';
import { ClientsActionMenuComponent } from '../clients-action-menu/clients-action-menu.component';

@Component({
    selector: 'app-clients-page',
    templateUrl: './clients-page.component.html',
    styleUrls: ['./clients-page.component.scss'],
    imports: [
    
    ClientsActionMenuComponent,
    ClientListComponent,
    AsyncPipe
]
})
export class ClientsComponent {}
