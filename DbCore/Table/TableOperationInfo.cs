using DbCore.Table.Operations;

namespace DbCore.Table;

public record TableOperationInfo
{
    public required ICompletedOperation Operation { get; init; }
}
