using DbCore.Raw;
using DbCore.Table;
using DbCore.Table.Constraints;

namespace DbCore.Schema;
public record SchemaColumn
{
    public required IConstraint[] Constraints { get; init; } 
    public ColType Type { get;}
    private readonly IColumn defaultValue;
    public IColumn DefaultValue => defaultValue.CreateCopy();

    public SchemaColumn(ColType type)
    {
        Type = type;
        defaultValue = DbTypeHelper.GetDefaultValue(Type);
    }

    public SchemaColumn(ColType type,IColumn _defaulValue)
    {
        Type = type;
        defaultValue = _defaulValue;
        if(!DbTypeHelper.IsHasCorrectType(Type, _defaulValue))
        {
            throw new Exception("schema collumn has incorrect type");
        }
    }
}