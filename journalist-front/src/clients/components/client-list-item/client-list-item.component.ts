import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

import { Router } from '@angular/router';
import { Client } from 'src/models/generated/graphql';

@Component({
    selector: 'app-client-list-item',
    imports: [TranslocoModule],
    templateUrl: './client-list-item.component.html',
    styleUrls: ['./client-list-item.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ClientListItemComponent {
  constructor(private _router: Router) {}

  @Input({ required: true }) public client: Client | null = null;

  public onRowClick(client: Client | null) {
    if (!client) return;

    this._router.navigate(['/clients', client.id]);
  }
}
