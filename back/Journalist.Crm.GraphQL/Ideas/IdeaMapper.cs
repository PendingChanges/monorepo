using Journalist.Crm.Domain.Ideas.DataModels;
using Journalist.Crm.GraphQL.Ideas.Inputs;
using Journalist.Crm.GraphQL.Ideas.Outputs;

namespace Journalist.Crm.GraphQL.Ideas;

public static class IdeaMapper
{
    public static Idea? ToIdeaOrNull(this IdeaDocument? ideaDocument)
        => ideaDocument?.ToIdea();

    public static Idea ToIdea(this IdeaDocument ideaDocument) =>
        new(ideaDocument.Id, ideaDocument.Name, ideaDocument.Description, ideaDocument.OwnerId,
            ideaDocument.PitchesIds.Count);

    public static IReadOnlyList<Idea> ToIdeas(this IReadOnlyList<IdeaDocument> ideas)
        => ideas.Select(ToIdea).ToList();

    public static Domain.Ideas.Commands.CreateIdea ToCommand(this CreateIdea createIdea)
        => new(createIdea.Name, createIdea.Description);

    public static Domain.Ideas.Commands.DeleteIdea ToCommand(this DeleteIdea deleteIdea)
        => new(deleteIdea.Id);

    public static Domain.Ideas.Commands.ModifyIdea ToCommand(this ModifyIdea modifyIdea)
        => new(modifyIdea.Id, modifyIdea.NewName, modifyIdea.NewDescription);
}
