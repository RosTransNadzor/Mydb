namespace DbCore.Table.Constraints;

public interface IConstraint
{
    public bool ApplyConstraint(ApplyConstrantInfo info);
}