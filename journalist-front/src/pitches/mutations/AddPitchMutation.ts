import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';
import {
  PitchAddedPayload,
  CreatePitchInput,
} from 'src/models/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class AddPitchMutation extends Mutation<
  { addPitch: PitchAddedPayload },
  CreatePitchInput
> {
  override document = gql`
    mutation addPitch(
      $content: PitchContentInput!
      $deadLineDate: DateTime
      $issueDate: DateTime
      $clientId: String!
      $ideaId: String!
    ) {
      addPitch(
        createPitch: {
          content: $content
          deadLineDate: $deadLineDate
          issueDate: $issueDate
          clientId: $clientId
          ideaId: $ideaId
        }
      ) {
        pitchId
      }
    }
  `;
}
