using System.Reflection;
using CodeReviewExamples.Production.Calculations;
using CodeReviewExamples.Production.Calculations.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace CodeReviewExamples.Tests.Calculations;

public class CalculatorTests
{
    [Fact]
    public void CalculateLeaseRate1_AsExpected()
    {
        const decimal factor = 1.5m;
        const decimal installment = 100m;
        const decimal expected = factor * installment * Calculator.GrenkePremiumSurcharge;

        var stubRepo = new StubRepository(factor);
        var sut = new Calculator(stubRepo);
        var actual = sut.CalculateLeaseRate(installment, Guid.NewGuid());

        actual.Should().Be(expected);
    }
    
    [Fact]
    public void CalculateLeaseRate2_AsExpected()
    {
        const decimal factor = 5.5m;
        const decimal installment = 700m;
        const decimal expected = factor * installment * Calculator.GrenkePremiumSurcharge;

        var stubRepo = new StubRepository(factor);
        var sut = new Calculator(stubRepo);
        var actual = sut.CalculateLeaseRate(installment, Guid.NewGuid());

        actual.Should().Be(expected);
    }
    
    [Fact]
    public void CalculateLeaseRate_RepoIsCalledWithCorrectId()
    {
        const decimal factor = 1.5m;
        const decimal installment = 100m;
        const decimal expected = factor * installment * Calculator.GrenkePremiumSurcharge;
        
        var conditionListId = Guid.NewGuid();
        var mockRepo = new Mock<IConditionListRepository>(MockBehavior.Strict);
        mockRepo.Setup(x => x.GetConditionListById(conditionListId))
            .Returns(new ConditionList(factor));
        var sut = new Calculator(mockRepo.Object);

        var actual = sut.CalculateLeaseRate(installment, conditionListId);

        actual.Should().Be(expected);
        mockRepo.Verify(x => x.GetConditionListById(conditionListId), Times.Once);
        mockRepo.VerifyNoOtherCalls();
    }
    
    [Fact]
    public void DetermineFactor_ReturnsCorrectFactor()
    {
        const decimal expected = 1.5m;
        var sut = new Calculator(new StubRepository(expected));
        var methodInfo = typeof(Calculator).GetMethod("DetermineFactor", BindingFlags.NonPublic | BindingFlags.Instance);
        var result = methodInfo!.Invoke(sut, [Guid.NewGuid()]);
        result!.Should().Be(1.5);
    }
    
    [Fact (Skip = "Not working")]
    public void ReverseCalculateFactor_ReturnsCorrectFactor()
    {
        const decimal expectedFactor = 5.5m;
        const decimal installment = 700m;
        
        var stubRepo = new StubRepository(expectedFactor);
        var sut = new Calculator(stubRepo);
        var actual = sut.ReverseCalculateFactor(installment, Guid.NewGuid());

        actual.Should().Be(expectedFactor);
    }
    
    private class StubRepository(decimal factor) 
        : IConditionListRepository
    {
        public ConditionList GetConditionListById(
            Guid conditionListId)
        {
            return new ConditionList(factor);
        }

        public void SaveConditionList(ConditionList conditionList)
        {
            throw new NotImplementedException();
        }
    }
}