namespace CodeReviewExamples.Production.Calculations.Services;

public class ConditionListRepository : IConditionListRepository
{
    //sample implementation...
    private readonly Dictionary<Guid, ConditionListDbo> _conditionListDatabases = new();
    private readonly GrenkeLogger _logger = GrenkeLogger.Instance;
    
    public ConditionList GetConditionListById(Guid conditionListId)
    {
        if (!_conditionListDatabases.TryGetValue(conditionListId, out var conditionListDbo))
            throw new ConditionListNotFoundException(conditionListId);

        var conditionList = new ConditionList(conditionListDbo.Factor, DateTime.Now);
        return conditionList;
    }

    public void SaveConditionList(ConditionList conditionList)
    {
        if (_conditionListDatabases.TryGetValue(conditionList.ConditionListId, out _))
            throw new ConditionListAlreadyExistsException(conditionList.ConditionListId);

        //mapping logic (domain -> dbo)
        var conditionListDbo = new ConditionListDbo(conditionList.Factor);
        //...
        
        _conditionListDatabases.Add(conditionList.ConditionListId, conditionListDbo);
        _logger.Log($"Added {conditionList.ConditionListId} to database");
    }
}