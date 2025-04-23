import type { Preview } from "@storybook/react";

import { Provider } from "../src/providers/provider";

import React from "react";
import '../src/app/globals.css'; 

const preview: Preview = {
  parameters: {
    options: {
      storySort: {
        method: "alphabetical",
      },
    },
  },
  decorators: [
    (Story) => {
      return (
        <Provider
          attribute="class"
          defaultTheme="system"
          enableSystem
          disableTransitionOnChange
          locale="en"
        >
          <Story />
        </Provider>
      );
    },
  ],
};

export default preview;
