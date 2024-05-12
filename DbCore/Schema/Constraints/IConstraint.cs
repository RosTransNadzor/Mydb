using DbCore.Raw;

namespace DbCore.Table.Constraints;

public interface IConstraint
{
    public bool ApplyConstraint(IColumn column);
}