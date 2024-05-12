using DbCore.Raw;
using DbCore.Table;
using DbCore.Table.Constraints;

namespace DbCore.Schema;
public record SchemaColumn
{
    public required IConstraint[] Constraints { get; init; } 
    public ColType Type { get;}
    public IColumn DefaultValue { get; }

    public SchemaColumn(ColType type)
    {
        Type = type;
        DefaultValue = DbTypeHelper.GetDefaultValue(Type);
    }

    public SchemaColumn(ColType type,IColumn defaulValue)
    {
        Type = type;
        DefaultValue = defaulValue;
        if(!DbTypeHelper.IsHasCorrectType(Type, defaulValue))
        {
            throw new Exception("schema collumn has incorrect type");
        }
    }
}