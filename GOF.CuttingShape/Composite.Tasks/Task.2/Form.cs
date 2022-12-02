namespace Composite.Tasks.Task._2;

public class Form : Composite
{
    private readonly string _name;
    
    public Form(string name)
    {
        _name = name;
    }
    
    public void AddComponent(Composite component)
    {
        _components.Add(component);
    }

    public override string ConvertToString()
    {
        _sb.Append($"<form name='{_name}'>");
        _sb.Append("\n\r");
        foreach (var component in _components)
        {
            _sb.Append(component.ConvertToString());
            _sb.Append("\n\r");
        }

        _sb.Append($"</form>");
        return _sb.ToString();
    } 
}