using DbCore.Raw;
namespace DbCore.Table;

public record ValidatedRow
{
    public required IColumn[] Columns { get; init; }
    public Row AsRow()
    {
        return new Row{Columns = Columns};
    }
}