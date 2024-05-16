namespace DbCore.Table.Operations;

public record RowInserted : ICompletedOperation
{
    public required ValidatedRow InsertedRow { get; init; }
}