namespace DbCore.Table;

public record ColType
{
    public required DbType Type { get; init; }
    public bool IsNullable { get; init; }
}