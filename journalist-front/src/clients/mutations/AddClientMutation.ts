import { Injectable } from '@angular/core';
import { Mutation, gql } from 'apollo-angular';
import { ClientAddedPayload, CreateClientInput } from 'src/models/generated/graphql';

@Injectable({
  providedIn: 'root',
})
export class AddClientMutation extends Mutation<
  { addClient: ClientAddedPayload },
  CreateClientInput
> {
  override document = gql`
    mutation addClient($name: String!) {
      addClient(createClient: { name: $name }) {
        clientId
      }
    }
  `;
}
