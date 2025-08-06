using CodeReviewExamples.Production.Calculations.Services;

namespace CodeReviewExamples.Production.Calculations;

public class Calculator(IConditionListRepository repository)
{
    public const decimal GrenkePremiumSurcharge = 1.05m;
    
    public decimal CalculateLeaseRate(
        decimal baseInstallment, 
        Guid conditionListId)
    {
        var factor = DetermineFactor(conditionListId);
        return baseInstallment * factor * GrenkePremiumSurcharge;
    }

    public decimal ReverseCalculateFactor(decimal leaseRate, Guid conditionListId)
    {
        throw new NotImplementedException();
    }
    
    private decimal DetermineFactor(Guid conditionListId) 
    {
        return repository
            .GetConditionListById(conditionListId).Factor;
    }
}