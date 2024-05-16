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

    public static bool Compare(IColumn col1, IColumn col2)
    {
        switch (col1)
        {
            case RowColumn<int> int1 when col2 is RowColumn<int> int2:
                return int1.Value == int2.Value;
            case RowColumn<int> int1 when col2 is RowColumn<int?> int2:
                return int1.Value == int2.Value;
            
            case RowColumn<int?> int1 when col2 is RowColumn<int> int2:
                return int1.Value == int2.Value;
            case RowColumn<int?> int1 when col2 is RowColumn<int?> int2:
                return int1.Value == int2.Value;
            
            case RowColumn<string> str1 when col2 is RowColumn<string> str2:
                return str2.Value == str1.Value;
            default:
                return false;
        }
    }
}