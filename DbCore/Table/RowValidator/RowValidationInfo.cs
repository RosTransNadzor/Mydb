using DbCore.Raw;
using DbCore.Table.Operations;

namespace DbCore.Table;

public record RowValidationInfo(IOperation Operation,Schema Schema,Row Row);