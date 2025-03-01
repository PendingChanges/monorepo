using CQRS;
using Journalist.Crm.Domain.Pitches.Events;

namespace Journalist.Crm.Domain.Pitches;

public class Pitch : Aggregate
{
    public PitchContent Content { get; private set; }
    public DateTime? DeadLineDate { get; private set; }
    public DateTime? IssueDate { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid IdeaId { get; private set; }
    public string OwnerId { get; private set; }

    private PitchStateMachine _stateMachine;

    public string CurrentState => _stateMachine.CurrentState;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Pitch() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public AggregateResult Create(PitchContent content, DateTime? deadLineDate, DateTime? issueDate,
        Guid clientId, Guid ideaId, string ownerId)
    {
        var result = AggregateResult.Create();

        var id = Guid.NewGuid();

        var @event = new PitchCreated(id, content, deadLineDate, issueDate, clientId, ideaId, ownerId);

        Apply(@event);
        result.AddEvent(@event);

        return result;
    }

    public AggregateResult Cancel(string ownerId)
    {
        var result = AggregateResult.Create();

        result.CheckAndAddError(() => OwnerId != ownerId, WellKnownErrors.NotPitchOwner());
        result.CheckAndAddError(() => !_stateMachine.CanCancel(), WellKnownErrors.PitchNotCancellable());

        if (result.HasErrors)
        {
            return result;
        }

        var @event = new PitchCancelled(Id, ClientId, IdeaId);
        Apply(@event);
        result.AddEvent(@event);

        return result;
    }

    public AggregateResult Validate(string ownerId)
    {
        var result = AggregateResult.Create();

        result.CheckAndAddError(() => OwnerId != ownerId, WellKnownErrors.NotPitchOwner());
        result.CheckAndAddError(() => !_stateMachine.CanValidate(), WellKnownErrors.PitchNotValidatable());

        if (result.HasErrors)
        {
            return result;
        }

        var @event = new PitchReadyToSend(Id);
        Apply(@event);
        result.AddEvent(@event);

        return result;
    }

    public AggregateResult Send(string ownerId)
    {
        var result = AggregateResult.Create();

        result.CheckAndAddError(() => OwnerId != ownerId, WellKnownErrors.NotPitchOwner());
        result.CheckAndAddError(() => !_stateMachine.CanSend(), WellKnownErrors.PitchNotSendable());

        if (result.HasErrors)
        {
            return result;
        }

        var @event = new PitchSent(Id);
        Apply(@event);
        result.AddEvent(@event);

        return result;
    }

    public AggregateResult Accept(string ownerId)
    {
        var result = AggregateResult.Create();
        result.CheckAndAddError(() => OwnerId != ownerId, WellKnownErrors.NotPitchOwner());
        result.CheckAndAddError(() => !_stateMachine.CanAccept(), WellKnownErrors.PitchNotAcceptable());
        if (result.HasErrors)
        {
            return result;
        }
        var @event = new PitchAccepted(Id);
        Apply(@event);
        result.AddEvent(@event);
        return result;
    }

    public AggregateResult Refuse(string ownerId)
    {
        var result = AggregateResult.Create();
        result.CheckAndAddError(() => OwnerId != ownerId, WellKnownErrors.NotPitchOwner());
        result.CheckAndAddError(() => !_stateMachine.CanRefuse(), WellKnownErrors.PitchNotRefusable());
        if (result.HasErrors)
        {
            return result;
        }
        var @event = new PitchRefused(Id);
        Apply(@event);
        result.AddEvent(@event);
        return result;
    }

    public AggregateResult Modify(PitchContent content, DateTime? deadLineDate, DateTime? issueDate, Guid clientId, Guid ideaId, string ownerId)
    {
        var result = AggregateResult.Create();

        result.CheckAndAddError(() => OwnerId != ownerId, WellKnownErrors.NotPitchOwner());
        result.CheckAndAddError(() => !_stateMachine.CanModify(), WellKnownErrors.PitchNotModifiable());

        if (result.HasErrors)
        {
            return result;
        }

        if (content != Content)
        {
            var @event = new PitchContentChanged(Id, content);
            Apply(@event);
            result.AddEvent(@event);
        }

        if (deadLineDate != DeadLineDate)
        {
            var @event = new PitchDeadLineRescheduled(Id, deadLineDate);
            Apply(@event);
            result.AddEvent(@event);
        }

        if (issueDate != IssueDate)
        {
            var @event = new PitchIssueRescheduled(Id, issueDate);
            Apply(@event);
            result.AddEvent(@event);
        }

        if (clientId != ClientId)
        {
            var @event = new PitchClientChanged(Id, clientId);
            Apply(@event);
            result.AddEvent(@event);
        }

        if (ideaId != IdeaId)
        {
            var @event = new PitchIdeaChanged(Id, ideaId);
            Apply(@event);
            result.AddEvent(@event);
        }

        return result;
    }

    private void Apply(PitchContentChanged @event)
    {
        Content = @event.Content;

        IncrementVersion();
    }

    private void Apply(PitchCreated @event)
    {
        SetId(@event.Id);
        Content = @event.Content;
        DeadLineDate = @event.DeadLineDate;
        IssueDate = @event.IssueDate;
        ClientId = @event.ClientId;
        IdeaId = @event.IdeaId;
        OwnerId = @event.OwnerId;
        _stateMachine = new PitchStateMachine(PitchStates.Draft);

        IncrementVersion();
    }

#pragma warning disable S1172 // Unused method parameters should be removed
    private void Apply(PitchCancelled @event)
    {
        _stateMachine.SetStatus(PitchStates.Cancelled);
        IncrementVersion();
    }

    private void Apply(PitchSent @event)

    {
        _stateMachine.SetStatus(PitchStates.Sent);
        IncrementVersion();
    }

    private void Apply(PitchAccepted @event)
    {
        _stateMachine.SetStatus(PitchStates.Accepted);
        IncrementVersion();
    }

    private void Apply(PitchRefused @event)
    {
        _stateMachine.SetStatus(PitchStates.Refused);
        IncrementVersion();
    }

    private void Apply(PitchReadyToSend @event)
    {
        _stateMachine.SetStatus(PitchStates.ReadyToSend);
        IncrementVersion();
    }
#pragma warning restore S1172 // Unused method parameters should be removed

    private void Apply(PitchDeadLineRescheduled @event)
    {
        DeadLineDate = @event.DeadLineDate;
        IncrementVersion();
    }

    private void Apply(PitchIssueRescheduled @event)
    {
        IssueDate = @event.IssueDate;
        IncrementVersion();
    }

    private void Apply(PitchClientChanged @event)
    {
        ClientId = @event.ClientId;
        IncrementVersion();
    }

    private void Apply(PitchIdeaChanged @event)
    {
        IdeaId = @event.IdeaId;
        IncrementVersion();
    }

    public bool CanValidate() => _stateMachine.CanValidate();
    public bool CanModify() => _stateMachine.CanModify();
    public bool CanCancel() => _stateMachine.CanCancel();
    public bool CanSend() => _stateMachine.CanSend();
    public bool CanAccept() => _stateMachine.CanAccept();
    public bool CanRefuse() => _stateMachine.CanRefuse();
}
