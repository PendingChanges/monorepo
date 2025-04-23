import { AllSubjectsCollectionSegment } from "@/gql/graphql";
import { ArrowRightIcon, PlusIcon } from "lucide-react";
import { useState } from "react";
import {
  Dialog,
  DialogTrigger,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogFooter,
} from "@/components/molecules/dialog";
import { Button } from "@/components/atoms/button";
import { useRouter } from "next/navigation";
import { CreateSubjectForm } from "./create-subject-form";
import { useTranslations } from "next-intl";

export function SubjectList({
  subjects,
}: {
  subjects: AllSubjectsCollectionSegment | null | undefined;
}) {
  const t = useTranslations("subject-list");
  const router = useRouter();
  const [isDialogOpen, setIsDialogOpen] = useState(false);

  const handleNavigate = (subjectId: string) => {
    router.push(`/subjects/${subjectId}`);
  };

  const handleCreateSubject = (event: React.FormEvent) => {
    event.preventDefault();
    console.log("Subject created");
    setIsDialogOpen(false);
  };

  return (
    <div className="p-4">
      <div className="flex justify-end mb-4">
        <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
          <DialogTrigger asChild>
            <Button className="flex items-center gap-2">
              <PlusIcon className="h-5 w-5" />
              {t("add-subject")}
            </Button>
          </DialogTrigger>
          <DialogContent>
            <DialogHeader>
              <DialogTitle>{t("add-subject")}</DialogTitle>
            </DialogHeader>
            <CreateSubjectForm onSubmit={handleCreateSubject} />
            <DialogFooter>
              <Button type="submit" form="subject-form">
                {t("validate")}
              </Button>
              <Button
                variant="secondary"
                onClick={() => setIsDialogOpen(false)}
              >
                {t("cancel")}
              </Button>
            </DialogFooter>
          </DialogContent>
        </Dialog>
      </div>
      <ul className="space-y-4">
        {subjects?.items?.map((subject) => (
          <li
            key={subject.id}
            className="flex justify-between items-center p-4 border border-gray-300 rounded-lg shadow-sm hover:bg-gray-100 cursor-pointer transition"
            onClick={() => handleNavigate(subject.id)}
          >
            <div className="flex flex-col">
              <h2 className="text-lg font-semibold text-gray-800">
                {subject.title}
              </h2>
              <p className="text-sm text-gray-600">{subject.description}</p>
            </div>
            <div className="text-blue-500 text-xl">
              <ArrowRightIcon className="h-6 w-6" />
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
}
