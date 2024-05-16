namespace DbCore.Table.Operations;

public record RowUpdated : ICompletedOperation
{
    public int RowId { get; init; }
    public required ValidatedRow UpdatedRow { get; init; }
}