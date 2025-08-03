namespace CodeReviewExamples.Production.Calculations.Services;

public class ConditionListAlreadyExistsException(Guid conditionListId)
    : Exception($"ConditionList with Id {conditionListId} already exists.");