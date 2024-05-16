using System.Reactive.Linq;
using DbCore.Raw;
using DbCore.Table.Operations;

namespace DbCore.Table.Constraints;

public class AutoIncrementConstraint : IConstraint
{
    private int nextValue;
    private IDisposable? _insertUpdate;
    public bool ApplyConstraint(ApplyConstrantInfo info)
    {
        Table currentTable = info.Manager.GetTableByName("current");
        
        _insertUpdate ??= currentTable
            .ChangesObservable
            .Where(to => to.Operation is RowInserted)
            .Subscribe(_ =>
            {
                if (currentTable.Rows.Count > nextValue)
                    nextValue = currentTable.Rows.Count;
                else
                    nextValue++;
            });
        if (info.OperationType is InsertedOperation insert)
        {
            Row row = insert.NewRow;
            row.Columns[info.ColId] = new RowColumn<int> { Value = nextValue };
        }
        return true;
    }
}