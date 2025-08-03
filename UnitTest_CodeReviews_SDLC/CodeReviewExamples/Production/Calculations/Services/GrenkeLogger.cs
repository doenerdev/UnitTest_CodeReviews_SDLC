namespace CodeReviewExamples.Production.Calculations.Services;

public class GrenkeLogger
{
    private static GrenkeLogger _instance;
    private static readonly Lock Lock = new();
    private readonly List<string> _logMessages = [];
    
    public IReadOnlyList<string> LogMessages => _logMessages.AsReadOnly();
    
    private GrenkeLogger() {}
    
    public static GrenkeLogger Instance
    {
        get
        {
            lock (Lock)
            {
                return _instance ??= new GrenkeLogger();
            }
        }
    }
    
    public void Log(string message)
    {
        _logMessages.Add(message);
    }

    public string PrintMostRecent()
    {
        return _logMessages.LastOrDefault() ?? string.Empty;
    }
}