import { Injectable } from '@angular/core';
import { Version } from '../graphql/generated';

@Injectable({
  providedIn: 'root',
})
export class VersionService {
  public getFormatedVersion(version: Version | undefined): string {
    return version ? `${version.major}.${version.minor}` : '';
  }
}
