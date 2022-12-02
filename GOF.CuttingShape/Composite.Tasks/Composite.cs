using System.Text;

namespace Composite.Tasks;

public abstract class Composite
{
    protected readonly List<Composite> _components;
    protected readonly StringBuilder _sb;

    protected Composite()
    {
        _components = new List<Composite>(0);
        _sb = new StringBuilder();
    }

    public virtual string ConvertToString()
    {
        return _sb.ToString();
    }
}