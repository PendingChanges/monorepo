import { ChangeDetectionStrategy, Component, Input } from '@angular/core';


@Component({
    selector: 'app-loading-row',
    imports: [],
    templateUrl: './loading-row.component.html',
    styleUrls: ['./loading-row.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoadingRowComponent {
  @Input() public loading: boolean | null = false;
}
