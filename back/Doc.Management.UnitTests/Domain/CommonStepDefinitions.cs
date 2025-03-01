﻿using TechTalk.SpecFlow;
using Xunit;

namespace Doc.Management.UnitTests.Domain;

[Binding]
public class CommonStepDefinitions(AggregateContext aggregateContext)
{
    private readonly AggregateContext _aggregateContext = aggregateContext;

    [Then(@"An error with code ""([^""]*)"" is raised")]
    public void ThenAnErrorWithCodeIsRaised(string errorCode)
    {
        Assert.NotNull(_aggregateContext.Aggregate);
        var error = _aggregateContext.GetErrors().Find(e => e.Code == errorCode);

        Assert.NotNull(error);
    }

    [Then(@"No errors")]
    public void ThenNoErrors()
    {
        Assert.NotNull(_aggregateContext.Aggregate);
        var errors = _aggregateContext.GetErrors();

        Assert.Empty(errors);
    }

}
