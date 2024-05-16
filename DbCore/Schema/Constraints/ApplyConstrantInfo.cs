using DbCore.Database;
using DbCore.Raw;
using DbCore.Table.Operations;

namespace DbCore.Table.Constraints;

public class ApplyConstrantInfo
{
    public required IColumn Column { get; init; }
    public required TableManager Manager { get; init; }
    public required IOperation OperationType { get; init; }
    public required int ColId { get; init; }
}