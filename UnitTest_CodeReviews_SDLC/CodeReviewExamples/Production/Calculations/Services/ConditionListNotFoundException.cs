namespace CodeReviewExamples.Production.Calculations.Services;

public class ConditionListNotFoundException(Guid conditionListId)
    : Exception($"ConditionList Id {conditionListId} was not found.");