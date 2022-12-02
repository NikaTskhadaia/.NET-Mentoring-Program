namespace Composite.Tasks.Task._1;

public class InputText : Composite
{
    private readonly string _name;
    private readonly string _value;

    public InputText(string name, string value)
    {
        _name = name;
        _value = value;
    }

    public override string ConvertToString()
    {
        return $"<inputText name='{_name}' value='{_value}'/>";
    }
}