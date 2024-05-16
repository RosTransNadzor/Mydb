namespace DbCore.Table.Operations;

public record RowDeleted : ICompletedOperation
{
    public required int RowId { get; init; }
}