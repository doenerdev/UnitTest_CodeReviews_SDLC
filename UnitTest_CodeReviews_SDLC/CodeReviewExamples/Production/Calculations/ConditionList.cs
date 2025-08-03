namespace CodeReviewExamples.Production.Calculations;

public class ConditionList(decimal factor)
{
    public ConditionList(decimal factor, DateTime retrievedAt) : this(factor)
    {
        RetrievedAt = retrievedAt;
    }

    public Guid ConditionListId { get; } = Guid.NewGuid();
    public decimal Factor { get; } = factor;
    public DateTime RetrievedAt { get; }
}