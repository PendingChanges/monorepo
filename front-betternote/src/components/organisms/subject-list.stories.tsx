import type { Meta, StoryObj } from "@storybook/react";

import { SubjectList } from "./subject-list";

const meta: Meta<typeof SubjectList> = {
  component: SubjectList,
  parameters: {
    nextjs: {
      appDirectory: true,
    },
  },
};

export default meta;
type Story = StoryObj<typeof SubjectList>;

export const Primary: Story = {
  args: {
    subjects: {
      items: [
        {
          id: "1",
          title: "Subject 1",
          description: "Description 1",
        },
        {
          id: "2",
          title: "Subject 2",
          description: "Description 2",
        },
      ],
      pageInfo: {
        hasNextPage: false,
        hasPreviousPage: false,
      },
      totalCount: 2,
    },
  },
};
