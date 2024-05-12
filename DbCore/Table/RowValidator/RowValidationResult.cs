using OneOf;
using OneOf.Types;

namespace DbCore.Table;

[GenerateOneOf]
public partial class RowValidationResult : OneOfBase<ValidatedRow, Error<string>>;
