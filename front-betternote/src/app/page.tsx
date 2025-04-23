"use client";

import { useQuery } from "@apollo/client";
import { graphql } from "../gql";
import { SubjectList } from "@/components/organisms/subject-list";

const allSubjects = graphql(/* GraphQL */ `
  query allSubjects {
    allSubjects {
      items {
        id
        title
        description
      }
      pageInfo {
        hasNextPage
        hasPreviousPage
      }
      totalCount
    }
  }
`);

export default function Home() {
  const { data, loading, error } = useQuery(allSubjects);

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error: {error.message}</div>;

  return (
    <div className="flex justify-center items-center h-screen">
      <div className="w-[600px] h-[400px] border border-gray-300 p-4 shadow-md">
        <h1 className="text-2xl font-semibold text-gray-800">Subjects</h1>
        <SubjectList subjects={data?.allSubjects} />
      </div>
    </div>
  );
}
