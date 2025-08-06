using System.Reflection;
using CodeReviewExamples.Production.Calculations;
using CodeReviewExamples.Production.Calculations.Services;
using FluentAssertions;
using Xunit;

namespace CodeReviewExamples.Tests.Calculations.Services;

public class ConditionListRepositoryTests
{
    [Fact]
    public void GetConditionListById_ReturnsExpectedConditionList()
    {
        const decimal expectedFactor = 1.0m;
        var expectedRetrievedAt = DateTime.Now;
        var conditionList = new ConditionList(expectedFactor);
        var sut = new ConditionListRepository();
        sut.SaveConditionList(conditionList);

        var actual = sut.GetConditionListById(conditionList.ConditionListId);
        actual.Factor.Should().Be(expectedFactor);
        actual.RetrievedAt.Should().Be(expectedRetrievedAt);
    }
    
    [Fact]
    public void SaveConditionList_SavesConditionListAndLogsMessage()
    {
        const decimal expectedFactor = 1.0m;
        var conditionList = new ConditionList(expectedFactor);
        var sut = new ConditionListRepository();
        sut.SaveConditionList(conditionList);

        var actual = sut.GetConditionListById(conditionList.ConditionListId);
        actual.Factor.Should().Be(expectedFactor);
        GrenkeLogger.Instance.PrintMostRecent().Should().Be($"Added {conditionList.ConditionListId} to database");
        GrenkeLogger.Instance.LogMessages.Count().Should().Be(1);
    }
    
    [Fact]
    public void SaveConditionList_AlreadyExists_NothingIsSavedOrLogged()
    {
        const decimal expectedFactor = 1.0m;
        var conditionList = new ConditionList(expectedFactor);
        var sut = new ConditionListRepository();
        sut.SaveConditionList(conditionList);
        //reset logger instance
        var loggerInstance = typeof(GrenkeLogger).GetField("_instance", BindingFlags.NonPublic | BindingFlags.Static);
        loggerInstance!.SetValue(GrenkeLogger.Instance, null);
        
        var action = () => sut.SaveConditionList(conditionList);
        action.Should().Throw<ConditionListAlreadyExistsException>();
        GrenkeLogger.Instance.LogMessages.Count().Should().Be(0);
    }
}