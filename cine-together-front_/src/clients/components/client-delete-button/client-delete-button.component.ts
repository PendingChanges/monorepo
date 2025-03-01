import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  ConfirmDialogComponent,
  ConfirmDialogModel,
} from 'src/common/components/confirm-dialog/confirm-dialog.component';
import { Client, DeleteClientInput } from 'src/models/generated/graphql';
import { ClientsService } from 'src/clients/services/ClientsService';
import { TranslocoModule } from '@ngneat/transloco';
import { Store } from '@ngrx/store';
import { ClientsActions } from 'src/clients/state/clients.actions';

@Component({
  selector: 'app-client-delete-button',
  templateUrl: './client-delete-button.component.html',
  styleUrls: ['./client-delete-button.component.scss'],
  standalone: true,
  imports: [TranslocoModule],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ClientDeleteButtonComponent {
  @Input({ required: true }) public client: Client | null = null;
  @Input() public disabled = false;
  constructor(private _modalService: NgbModal, private _store: Store) {}

  openConfirmDialog(): void {
    const dialogRef = this._modalService.open(ConfirmDialogComponent);
    dialogRef.componentInstance.data = new ConfirmDialogModel(
      `Confirm ${this.client?.name} deletion`,
      `Are you sure you want to delete client ${this.client?.name} ?`
    );

    dialogRef.closed.subscribe((dialogResult) => {
      if (dialogResult && this.client) {
        this._store.dispatch(
          ClientsActions.removeClient(<DeleteClientInput>{
            id: this.client.id,
          })
        );
      }
    });
  }
}
