using DbCore.Raw;

namespace DbCore.Table;

public class DbTypeHelper
{
    public static bool IsHasCorrectType(ColType type,IColumn column)
    {
        switch (type.Type)
        {
            case DbType.Int32 when type.IsNullable == false:
                return column is RowColumn<int>;
            case DbType.Int32 when type.IsNullable:
                return column is RowColumn<int?> or RowColumn<int>;
                
            case DbType.String when type.IsNullable:
                return column is RowColumn<string>;
            case DbType.String when type.IsNullable == false: 
                return column is RowColumn<string> { Value: not null };
            default: 
                return false;
        }
    }
    public static IColumn GetDefaultValue(ColType type)
    {
        switch (type.Type)
        {
            case DbType.Int32 when type.IsNullable == false:
                return new RowColumn<int> { Value = 0 };
            case DbType.Int32 when type.IsNullable:
                return new RowColumn<int?>{Value = null};
                
            case DbType.String when type.IsNullable:
                return new RowColumn<string?> { Value = null };
            case DbType.String when type.IsNullable == false:
                return new RowColumn<string> { Value = string.Empty };
            default:
                throw new Exception("Undocumented DbType");
        }
    }
}