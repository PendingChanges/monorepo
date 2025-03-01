import { ChangeDetectionStrategy, Component } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {
  SaveClientComponent,
  SaveClientModel,
} from '../save-client/save-client.component';
import { TranslocoModule } from '@ngneat/transloco';

@Component({
    selector: 'app-add-client-button',
    templateUrl: './add-client-button.component.html',
    styleUrls: ['./add-client-button.component.scss'],
    standalone: true,
    imports: [TranslocoModule],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddClientButtonComponent {
  constructor(private _modalService: NgbModal) {}
  openDialog(): void {
    const dialogRef = this._modalService.open(SaveClientComponent);
    dialogRef.componentInstance.data = new SaveClientModel('add', null);
  }
}
