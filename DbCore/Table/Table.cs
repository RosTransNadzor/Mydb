using System.Reactive.Subjects;
using DbCore.Database;
using DbCore.Table.Operations;

namespace DbCore.Table;
using Raw;

public class Table
{
    public readonly Subject<TableOperationInfo> ChangesObservable = new();
    public required Schema Schema { get; init; }
    public IList<ValidatedRow> Rows { get; } = [];

    private readonly RowValidator _validator;

    public void Print()
    {
        foreach (var key in Schema.Columns.Keys)
        {
           Console.Write($"{key}\t\t"); 
        }
        Console.WriteLine();
        foreach (var row in Rows)
        {
            
            Console.WriteLine(row);
        }
    }
    public Table(TableManager manager)
    {
        manager.SetCurrent(this);
        _validator = new RowValidator(manager);
    }

    public void DeleteRow(int index)
    {
        Rows.RemoveAt(index);
        ChangesObservable.OnNext(new TableOperationInfo
        {
            Operation = new RowDeleted
            {
                RowId = index
            }
        });
    }
    public void InsertValues(Dictionary<string,IColumn> values)
    {
        Row row = Schema.FormRowFromValues(values);
        var result = _validator.ValidateRow(new(new InsertedOperation{NewRow = row},Schema,row));

        if (result.IsT0)
        {
            var validatedRow = result.AsT0;
            Rows.Add(result.AsT0);
            ChangesObservable.OnNext(new TableOperationInfo
            {
                Operation = new RowInserted
                {
                    InsertedRow = validatedRow
                }
            });
        }
        else if(result.IsT1)
            Console.WriteLine(result.AsT1.Value);
    }

    public void UpdateRow(int index,Dictionary<string,IColumn> values)
    {
        Row updated = Schema.UpdateRowFromValues(Rows[index], values);
        var result = _validator.ValidateRow(new(new UpdateOperation
        {
            RowId = index,UpdateRow = updated
        },Schema,updated));

        if (result.IsT0)
        {
            ValidatedRow validatedRow = result.AsT0;
            Rows[index] = validatedRow;
            ChangesObservable.OnNext(new TableOperationInfo
            {
                Operation = new RowUpdated
                {
                    RowId = index,
                    UpdatedRow = validatedRow
                }
            });
        }
        else if(result.IsT1)
            Console.WriteLine(result.AsT1.Value);
    }
}

