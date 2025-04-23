"use client";

import { ThemeProvider as NextThemesProvider } from "next-themes";
import { NextIntlClientProvider } from "next-intl";
import { CustomApolloProvider } from "./custom-apollo-provider";

export function Provider({
  children,
  ...props
}:
  | React.ComponentProps<typeof NextThemesProvider>
  | React.ComponentProps<typeof NextIntlClientProvider>) {
  return (
    <CustomApolloProvider>
      <NextIntlClientProvider {...props}>
        <NextThemesProvider {...props}>{children}</NextThemesProvider>
      </NextIntlClientProvider>
    </CustomApolloProvider>
  );
}
