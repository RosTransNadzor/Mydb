using DbCore.Raw;

namespace DbCore.Table.Operations;

public record InsertedOperation : IOperation
{
    public required Row NewRow { get; init; }

}