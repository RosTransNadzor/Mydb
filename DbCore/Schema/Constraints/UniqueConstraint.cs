using DbCore.Table.Operations;

namespace DbCore.Table.Constraints;

public class UniqueConstraint : IConstraint
{
    public bool ApplyConstraint(ApplyConstrantInfo info)
    {
        Table currentTable = info.Manager.GetTableByName("current");
        switch (info.OperationType)
        {
            case InsertedOperation insert:
            {
                return currentTable.Rows.All(vr => 
                    !DbTypeHelper.Compare(vr.Columns[info.ColId],info.Column));
            }
            case UpdateOperation update:
            {
                return currentTable.Rows
                    .Where((_, id) => id != update.RowId)
                    .All(vr => !DbTypeHelper.Compare(vr.Columns[info.ColId],info.Column));
            }
            case DeleteOperation delete:
                return true;
            default:
                return false;
        }
    }
}