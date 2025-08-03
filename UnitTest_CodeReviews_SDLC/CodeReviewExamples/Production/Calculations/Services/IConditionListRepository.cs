namespace CodeReviewExamples.Production.Calculations.Services;

public interface IConditionListRepository
{
    ConditionList GetConditionListById(Guid conditionListId);
    void SaveConditionList(ConditionList conditionList);
}