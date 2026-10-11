
public abstract class Goal
{
    private string _shortName;
    private string _description;
    private int _points;

    public Goal(string shortName, string description, int points)
    {
        _shortName = shortName;
        _description = description;
        _points = points;
    }

    public string GetName()
    {
        return _shortName;
    }

    public int GetPoints()
    {
        return _points;
    }

    public virtual string GetDetailsString()
    {
        return _description;
    }

    public string GetDisplayString()
    {
        string checkbox = IsComplete() ? "[X]" : "[ ]";
        return $"{checkbox} {GetDetailsString()}";
    }

    public abstract int RecordEvent();

    public abstract bool IsComplete();

    public abstract string GetStringRepresentation();
}

