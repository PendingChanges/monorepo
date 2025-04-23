"use client";

import { ApolloClient, ApolloProvider, InMemoryCache } from "@apollo/client";

import { ReactNode } from "react";

export function CustomApolloProvider({ children }: { children: ReactNode }) {
  const client = new ApolloClient({
    uri: "https://localhost:7215/graphql/",
    cache: new InMemoryCache(),
  });
  return <ApolloProvider client={client}>{children}</ApolloProvider>;
}
