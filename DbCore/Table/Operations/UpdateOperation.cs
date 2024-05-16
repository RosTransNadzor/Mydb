using DbCore.Raw;

namespace DbCore.Table.Operations;

public record UpdateOperation : IOperation
{
    public required Row UpdateRow { get; init; }
    public required int RowId { get; init; }
}