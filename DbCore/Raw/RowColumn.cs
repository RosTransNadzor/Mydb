namespace DbCore.Raw;

public record RowColumn<T> : IColumn
{
    public required T Value { get; init; }
}