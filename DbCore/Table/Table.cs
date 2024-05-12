namespace DbCore.Table;
using Raw;

public enum Operation
{
    Insert,
    Update
}

public class Table
{
    public required Schema Schema { get; init; }
    public IList<ValidatedRow> Rows { get; } = [];

    private readonly RowValidator _validator;

    public Table(RowValidator validator)
    {
        validator.Manager.SetCurrent(this);
        _validator = validator;
    }

    public void InsertValues(Dictionary<string,IColumn> values)
    {
        Row row = Schema.FormRowFromValues(values);
        var result = _validator.ValidateRow(row,Schema);
        
        if (result.IsT0)
            Rows.Add(result.AsT0);
        else if(result.IsT1)
            Console.WriteLine(result.AsT1.Value);
    }

    public void UpdateRow(int index,Dictionary<string,IColumn> values)
    {
        Row updated = Schema.UpdateRowFromValues(Rows[index], values);
        var result = _validator.ValidateRow(updated,Schema);
        
        if (result.IsT0)
            Rows[index] = result.AsT0;
        else if(result.IsT1)
            Console.WriteLine(result.AsT1.Value);
    }
}

