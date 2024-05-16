namespace DbCore.Raw;

public record RowColumn<T> : IColumn
{
    public required T Value { get; set; }

    public override string ToString()
    {
        return Value?.ToString() ?? "^null";
    }

    public IColumn CreateCopy()
    {
        return new RowColumn<T> { Value = Value };
    }
}