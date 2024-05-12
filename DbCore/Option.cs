using OneOf;
using OneOf.Types;

namespace DbCore;

[GenerateOneOf]
public partial class Option<T> : OneOfBase<T,None>;
