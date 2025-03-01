import { TranslocoRootModule } from './infrastructure/transloco-root.module';
import { EditorModule } from '@tinymce/tinymce-angular';
import {
  withInterceptorsFromDi,
  provideHttpClient,
} from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import {
  BrowserAnimationsModule,
  provideAnimations,
} from '@angular/platform-browser/animations';
import { ROUTES } from './infrastructure/routes';
import { BrowserModule, bootstrapApplication } from '@angular/platform-browser';
import {
  NgbDateAdapter,
  NgbDateNativeAdapter,
  NgbModule,
} from '@ng-bootstrap/ng-bootstrap';
import { AuthGuard } from './infrastructure/auth.guard';
import { KeycloakService, KeycloakAngularModule } from 'keycloak-angular';
import { initializeKeycloak } from './infrastructure/initializeKeycloak';
import { APP_INITIALIZER, importProvidersFrom, isDevMode } from '@angular/core';
import { environment } from 'src/infrastructure/environments/environment';
import { DefaultOptions, InMemoryCache } from '@apollo/client/core';
import { HttpLink } from 'apollo-angular/http';
import { APOLLO_OPTIONS, ApolloModule } from 'apollo-angular';
import { provideStore } from '@ngrx/store';
import { clientsReducer } from './clients/state/clients.reducer';
import { provideStoreDevtools } from '@ngrx/store-devtools';
import { provideEffects } from '@ngrx/effects';
import * as clientsEffects from './clients/state/clients.effects';
import * as ideasEffects from './ideas/state/ideas.effects';
import * as pitchesEffects from './pitches/state/pitches.effects';
import { ideasReducer } from './ideas/state/ideas.reducer';
import { provideRouter } from '@angular/router';
import { AppComponent } from './layout/components/app.component';
import { pitchesReducer } from './pitches/state/pitches.reducer';

const apolloDefaultOptions: DefaultOptions = {
  watchQuery: {
    fetchPolicy: 'no-cache',
    errorPolicy: 'ignore',
  },
  query: {
    fetchPolicy: 'no-cache',
    errorPolicy: 'all',
  },
};

bootstrapApplication(AppComponent, {
  providers: [
    importProvidersFrom(
      BrowserAnimationsModule,
      ReactiveFormsModule,
      FormsModule,
      EditorModule,
      ApolloModule,
      TranslocoRootModule,
      KeycloakAngularModule,
      NgbModule
    ),
    {
      provide: APOLLO_OPTIONS,
      useFactory(httpLink: HttpLink) {
        return {
          cache: new InMemoryCache({ resultCaching: false }),
          link: httpLink.create({
            uri: environment.graphqlUrl,
          }),
          defaultOptions: apolloDefaultOptions,
        };
      },
      deps: [HttpLink],
    },
    {
      provide: APP_INITIALIZER,
      useFactory: initializeKeycloak,
      multi: true,
      deps: [KeycloakService],
    },
    AuthGuard,
    { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter },
    provideRouter(ROUTES),
    provideAnimations(),
    provideHttpClient(withInterceptorsFromDi()),
    provideStore({
      clientState: clientsReducer,
      ideaState: ideasReducer,
      pitchState: pitchesReducer,
    }),
    provideEffects(clientsEffects, ideasEffects, pitchesEffects),
    provideStoreDevtools({
      maxAge: 25, // Retains last 25 states
      logOnly: !isDevMode(), // Restrict extension to log-only mode
      autoPause: true, // Pauses recording actions and state changes when the extension window is not open
      trace: false, //  If set to true, will include stack trace for every dispatched action, so you can see it in trace tab jumping directly to that part of code
      traceLimit: 75, // maximum stack trace frames to be stored (in case trace option was provided as true)
    }),
  ],
}).catch((err) => console.error(err));
