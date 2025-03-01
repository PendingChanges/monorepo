﻿using Journalist.Crm.Domain.Pitches.DataModels;
using Journalist.Crm.GraphQL.Pitches.Inputs;
using Journalist.Crm.GraphQL.Pitches.Outputs;

namespace Journalist.Crm.GraphQL.Pitches;

public static class PitchMapper
{
    public static Pitch? ToPitchOrNull(this PitchDocument? pitchDocument)
        => pitchDocument?.ToPitch();

    public static Pitch ToPitch(this PitchDocument pitchDocument)
        => new(pitchDocument.Id, pitchDocument.Content, pitchDocument.DeadLineDate, pitchDocument.IssueDate, pitchDocument.ClientId, pitchDocument.IdeaId, pitchDocument.OwnerId);

    public static IReadOnlyList<Pitch> ToPitches(this IReadOnlyList<PitchDocument> clients)
        => clients.Select(ToPitch).ToList();

    public static Domain.Pitches.Commands.CreatePitch ToCommand(this CreatePitch createPitch)
    => new(createPitch.Content, createPitch.DeadLineDate, createPitch.IssueDate, createPitch.ClientId, createPitch.IdeaId);

    public static Domain.Pitches.Commands.DeletePitch ToCommand(this DeletePitch deletePitch)
        => new(deletePitch.Id);

    public static Domain.Pitches.Commands.ModifyPitch ToCommand(this ModifyPitch modifyPitch)
     => new(modifyPitch.Id, modifyPitch.Content, modifyPitch.DeadLineDate, modifyPitch.IssueDate, modifyPitch.ClientId, modifyPitch.IdeaId);

    public static PitchGuards ToPitchGuards(this Domain.Pitches.Pitch pitch)
        => new(pitch.CanModify(), pitch.CanValidate(), pitch.CanCancel(), pitch.CanSend(), pitch.CanAccept(), pitch.CanRefuse());

    public static PitchGuards? ToPitchGuardsOrNull(this Domain.Pitches.Pitch? pitch)
        => pitch?.ToPitchGuards();
}
