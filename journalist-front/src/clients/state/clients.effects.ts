import { inject } from '@angular/core';
import { ApolloQueryResult } from '@apollo/client/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { MutationResult } from 'apollo-angular';
import { catchError, switchMap, map, of, tap } from 'rxjs';
import {
  AllClientsCollectionSegment,
  AllPitchesCollectionSegment,
  Client,
  ClientAddedPayload,
  QueryAllClientsArgs,
  QueryAllPitchesArgs,
} from 'src/models/generated/graphql';
import { ClientsService } from 'src/clients/services/ClientsService';
import { ClientsActions } from './clients.actions';
import { Router } from '@angular/router';
import { PitchesService } from 'src/pitches/services/PitchesService';

export const loadClients = createEffect(
  (actions$ = inject(Actions), clientsService = inject(ClientsService)) => {
    return actions$.pipe(
      ofType(ClientsActions.loadClientList),
      switchMap((a: { args: QueryAllClientsArgs; append: boolean }) => {
        return clientsService.getClients(a.args).pipe(
          map((clientListResult) =>
            ClientsActions.clientListLoadedSuccess({
              clients: clientListResult.data.allClients.items || [],
              append: a.append,
            })
          ),
          catchError((result: ApolloQueryResult<AllClientsCollectionSegment>) =>
            of(
              ClientsActions.clientListLoadedFailure({
                errors: result.errors?.map((e) => e.message) || [
                  'Unknown error',
                ],
              })
            )
          )
        );
      })
    );
  },
  { functional: true, dispatch: true }
);

export const loadClientPitches = createEffect(
  (actions$ = inject(Actions), pitchesService = inject(PitchesService)) => {
    return actions$.pipe(
      ofType(ClientsActions.loadClientPitchList),
      switchMap((a) => {
        pitchesService.refreshClientPitches(a.args);
        return pitchesService.currentClientPitchListResult$.pipe(
          map((pitchListResult) =>
            ClientsActions.clientPitchListLoadedSuccess({
              pitches: pitchListResult.data.allPitches.items || [],
            })
          ),
          catchError((result: ApolloQueryResult<AllPitchesCollectionSegment>) =>
            of(
              ClientsActions.clientPitchListLoadedFailure({
                errors: result.errors?.map((e) => e.message) || [
                  'Unknown error',
                ],
              })
            )
          )
        );
      })
    );
  },
  { functional: true, dispatch: true }
);

export const loadClient = createEffect(
  (actions$ = inject(Actions), clientsService = inject(ClientsService)) => {
    return actions$.pipe(
      ofType(ClientsActions.loadClient),
      switchMap(({ clientId }) => {
        return clientsService.getClient(clientId).pipe(
          map((clientResult) =>
            ClientsActions.clientLoadedSuccess({
              client: clientResult.data.client,
            })
          ),
          catchError((result: ApolloQueryResult<{ client: Client }>) =>
            of(
              ClientsActions.clientLoadedFailure({
                errors: result.errors?.map((e) => e.message) || [
                  'Unknown error',
                ],
              })
            )
          )
        );
      })
    );
  },
  { functional: true, dispatch: true }
);

export const addClient = createEffect(
  (actions$ = inject(Actions), clientsService = inject(ClientsService)) => {
    return actions$.pipe(
      ofType(ClientsActions.addClient),
      switchMap((addClient) => {
        return clientsService.addClient(addClient).pipe(
          map((addClientResult) =>
            ClientsActions.clientAddedSuccess({
              payload: <ClientAddedPayload>addClientResult.data?.addClient,
              args: <QueryAllClientsArgs>{
                skip: 0,
                sortBy: 'name',
                take: 10,
              },
              date: new Date(),
            })
          ),
          catchError(
            (result: MutationResult<{ addClient: ClientAddedPayload }>) =>
              of(
                ClientsActions.clientAddedFailure({
                  errors: result.errors?.map((e) => e.message) || [
                    'Unknown error',
                  ],
                })
              )
          )
        );
      })
    );
  },
  { functional: true, dispatch: true }
);

export const renameClient = createEffect(
  (actions$ = inject(Actions), clientsService = inject(ClientsService)) => {
    return actions$.pipe(
      ofType(ClientsActions.renameClient),
      switchMap((renameClient) => {
        return clientsService.renameClient(renameClient).pipe(
          map((renameClientResult) =>
            ClientsActions.clientRenamedSuccess({
              payload: <string>renameClientResult.data,
              newName: renameClient.newName,
            })
          ),
          catchError((result: MutationResult<string>) =>
            of(
              ClientsActions.clientRenamedFailure({
                errors: result.errors?.map((e) => e.message) || [
                  'Unknown error',
                ],
              })
            )
          )
        );
      })
    );
  },
  { functional: true, dispatch: true }
);

export const removeClient = createEffect(
  (actions$ = inject(Actions), clientsService = inject(ClientsService)) => {
    return actions$.pipe(
      ofType(ClientsActions.removeClient),
      switchMap((removeClient) => {
        return clientsService.removeClient(removeClient).pipe(
          map((removeClientResult) =>
            ClientsActions.clientRemovedSuccess({
              payload: <string>removeClientResult.data?.removeClient,
            })
          ),
          catchError((result: MutationResult<{ removeClient: string }>) =>
            of(
              ClientsActions.clientRemovedFailure({
                errors: result.errors?.map((e) => e.message) || [
                  'Unknown error',
                ],
              })
            )
          )
        );
      })
    );
  },
  { functional: true, dispatch: true }
);

export const redirectToClient = createEffect(
  (actions$ = inject(Actions), router = inject(Router)) => {
    return actions$.pipe(
      ofType(ClientsActions.clientAddedSuccess),
      tap((result) => {
        router.navigate(['/clients', result.payload.clientId]);
      })
    );
  },
  { functional: true, dispatch: false }
);

export const redirectToClients = createEffect(
  (actions$ = inject(Actions), router = inject(Router)) => {
    return actions$.pipe(
      ofType(ClientsActions.clientRemovedSuccess),
      tap(() => {
        router.navigate(['/clients']);
      })
    );
  },
  { functional: true, dispatch: false }
);
