import { CommonModule } from '@angular/common';
import {
  Component,
  computed,
  inject,
  Inject,
  input,
  OnInit,
} from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { ToolbarModule } from 'primeng/toolbar';

import { Document } from '../../graphql/generated';
import { environment } from '../../infrastructure/environments/environment.development';
import { DocumentIconComponent } from '../molecules/document-icon.component';
import { RouterModule } from '@angular/router';
import { MenuModule } from 'primeng/menu';
import { MenuItem } from 'primeng/api';
import { VersionService } from '../../services/version-service';

@Component({
  selector: 'app-document-list-item',
  standalone: true,
  imports: [
    ButtonModule,
    DocumentIconComponent,
    CommonModule,
    ToolbarModule,
    RouterModule,
    MenuModule,
  ],
  template: `<div
    class="flex hover:surface-100 cursor-pointer"
    [ngClass]="{ 'border-top-1 surface-border': !first() }"
  >
    <a
      [routerLink]="documentInfosUrl()"
      rel="noopener noreferrer"
      class="flex-grow-1 no-underline"
    >
      <div class="flex ">
        <div><app-document-icon [document]="document()" /></div>
        <div class="text-lg font-medium text-900">
          {{ document()?.name }}
        </div>
        <div>
          {{ this.versionService.getFormatedVersion(document()?.version) }}
        </div>
      </div>
    </a>
    <div>
      <a [href]="documentUrl()" target="_blank">
        <span
          class="p-button-icon pi pi-external-link"
          aria-hidden="true"
        ></span
      ></a>
    </div>
  </div> `,
  styles: '',
})
export class DocumentListItemComponent implements OnInit {
  public versionService = inject(VersionService);
  items: MenuItem[] | undefined;

  document = input<Document>();
  first = input<boolean>();
  documentUrl = computed(
    () => `${environment.apiUrl}/documents/${this.document()?.id}`
  );
  documentInfosUrl = computed(() => `/documents/${this.document()?.id}`);
  ngOnInit() {
    this.items = [
      {
        label: 'Open in new tab',
        icon: 'pi pi-external-link',
        url: this.documentUrl(),
      },
    ];
  }
}
