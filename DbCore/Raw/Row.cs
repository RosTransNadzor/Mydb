namespace DbCore.Raw;

public class Row
{
    public required IColumn[] Columns { get; init; }
}