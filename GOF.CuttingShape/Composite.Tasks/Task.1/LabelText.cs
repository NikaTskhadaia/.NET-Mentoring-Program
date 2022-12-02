namespace Composite.Tasks.Task._1;

public class LabelText : Composite
{
    private readonly string _value;

    public LabelText(string value)
    {
        _value = value;
    }

    public override string ConvertToString()
    {
        return $"<label value='{_value}'/>";
    }
}