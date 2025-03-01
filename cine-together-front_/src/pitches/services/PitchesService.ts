import { Injectable } from '@angular/core';
import { MutationResult, QueryRef } from 'apollo-angular';
import { map, Observable } from 'rxjs';
import {
  AllPitchesCollectionSegment,
  Pitch,
  PitchAddedPayload,
  DeletePitchInput,
  QueryAllPitchesArgs,
  ModifyPitchInput,
} from 'src/models/generated/graphql';
import { AddPitchMutation } from 'src/pitches/mutations/AddPitchMutation';
import { CreatePitchInput } from 'src/models/generated/graphql';
import { AllPitchesQuery } from 'src/pitches/queries/AllPitchesQuery';
import { PitchQuery } from 'src/pitches/queries/PitchQuery';
import { ApolloQueryResult } from '@apollo/client/core';
import { DeletePitchMutation } from '../mutations/DeletePitchMutation';
import { ModifyPitchMutation } from '../mutations/ModifyPitchMutation';

@Injectable({
  providedIn: 'root',
})
export class PitchesService {
  private _allPitchesQueryRef: QueryRef<
    { allPitches: AllPitchesCollectionSegment },
    QueryAllPitchesArgs
  > | null = null;

  private _currrentClientallPitchesQueryRef: QueryRef<
    { allPitches: AllPitchesCollectionSegment },
    QueryAllPitchesArgs
  > | null = null;

  private _currrentIdeaallPitchesQueryRef: QueryRef<
    { allPitches: AllPitchesCollectionSegment },
    QueryAllPitchesArgs
  > | null = null;

  constructor(
    private _allPitchesQuery: AllPitchesQuery,
    private _pitchQuery: PitchQuery,
    private _addPitchMutation: AddPitchMutation,
    private _deletePitchMutation: DeletePitchMutation,
    private _modifyPitchMutation: ModifyPitchMutation
  ) {
    this._allPitchesQueryRef = this._allPitchesQuery.watch();
    this._currrentClientallPitchesQueryRef = this._allPitchesQuery.watch();
    this._currrentIdeaallPitchesQueryRef = this._allPitchesQuery.watch();
    this.pitchListResult$ = this._allPitchesQueryRef.valueChanges;
    this.currentClientPitchListResult$ =
      this._currrentClientallPitchesQueryRef.valueChanges;
    this.currentIdeaPitchListResult$ =
      this._currrentIdeaallPitchesQueryRef.valueChanges;
  }

  public pitchListResult$: Observable<
    ApolloQueryResult<{ allPitches: AllPitchesCollectionSegment }>
  >;

  public currentClientPitchListResult$: Observable<
    ApolloQueryResult<{ allPitches: AllPitchesCollectionSegment }>
  >;

  public currentIdeaPitchListResult$: Observable<
    ApolloQueryResult<{ allPitches: AllPitchesCollectionSegment }>
  >;

  public refreshPitches(args: QueryAllPitchesArgs): void {
    this._allPitchesQueryRef?.refetch(args);
  }

  public getPitch(id: string) {
    return this._pitchQuery.watch({
      id: id,
    }).valueChanges;
  }

  public addPitch(
    value: CreatePitchInput
  ): Observable<MutationResult<{ addPitch: PitchAddedPayload }>> {
    return this._addPitchMutation.mutate(value);
  }

  public removePitch(
    deletePitchInput: DeletePitchInput
  ): Observable<MutationResult<{ removePitch: string }>> {
    return this._deletePitchMutation.mutate(deletePitchInput);
  }

  public modifyPitch(
    value: ModifyPitchInput
  ): Observable<MutationResult<string>> {
    return this._modifyPitchMutation.mutate(value);
  }

  public refreshClientPitches(args: QueryAllPitchesArgs): void {
    this._currrentClientallPitchesQueryRef?.refetch(args);
  }

  public refreshIdeaPitches(args: QueryAllPitchesArgs): void {
    this._currrentIdeaallPitchesQueryRef?.refetch(args);
  }
}
