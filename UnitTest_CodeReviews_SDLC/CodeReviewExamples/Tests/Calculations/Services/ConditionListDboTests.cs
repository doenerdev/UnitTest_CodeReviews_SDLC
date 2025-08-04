using CodeReviewExamples.Production.Calculations.Services;
using FluentAssertions;
using Xunit;

namespace CodeReviewExamples.Tests.Calculations.Services;

public class ConditionListDboTests
{
    [Fact]
    public void Ctor_AsExpected()
    {
        var sut = new ConditionListDbo(1.0m);
        
        sut.Should().NotBeNull();
        sut.Should().BeOfType<ConditionListDbo>();
        sut.Factor.Should().Be(1.0m);
    }
}