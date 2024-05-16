using DbCore.Raw;
namespace DbCore.Table;

public record ValidatedRow
{
    public required IColumn[] Columns { get; init; }

    public override string ToString()
    {
        return string.Join("\t\t",Columns.Select(c => c.ToString()!).ToArray());
    }
    public Row AsRow()
    {
        return new Row{Columns = Columns};
    }
}