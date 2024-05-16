using DbCore.Raw;
using DbCore.Schema;

namespace DbCore.Table;

public record Schema
{
    public required Dictionary<string,SchemaColumn> Columns { get; init; }

    public Row FormRowFromValues(Dictionary<string, IColumn> values)
    {
        if (values.Keys.Any(s => !Columns.ContainsKey(s)))
            throw new Exception("values contains undocumented col");

        IColumn[] cols = Columns.Select(kvp => 
            values.TryGetValue(kvp.Key, out var column) 
            ? column
            : kvp.Value.DefaultValue)
            .ToArray();

        return new Row { Columns = cols };
    }

    public Row UpdateRowFromValues(ValidatedRow existing,Dictionary<string, IColumn> vals)
    {
        IColumn[] updatedCols = new IColumn[existing.Columns.Length];
        
        if (vals.Keys.Any(s => !Columns.ContainsKey(s)))
            throw new Exception("values contains undocumented col");

        for (int i = 0; i < existing.Columns.Length; i++)
        {
            if (vals.ContainsKey(Columns.ElementAt(i).Key))
                updatedCols[i] = vals[Columns.ElementAt(i).Key];
            else
                updatedCols[i] = existing.Columns[i];
        }

        return new Row { Columns = updatedCols };
    }
}