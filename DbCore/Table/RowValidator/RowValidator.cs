using DbCore.Database;
using DbCore.Raw;
using DbCore.Schema;
using OneOf.Types;

namespace DbCore.Table;

public class RowValidator
{
    public  readonly TableManager  Manager;
    public RowValidator(TableManager manager)
    {
        Manager = manager;
    }
    public RowValidationResult ValidateRow(Row row,Schema schema)
    {
        if(!row.Columns
               .Zip(schema.Columns.Values)
               .All(t => ValidateCollumn(t.First,t.Second))
          )
        {
            return new Error<string>("some validation errors was occured");
        }
        else
        {
            return new ValidatedRow
            {
                Columns = row.Columns
            };
        }
    }
    private bool ValidateCollumn(IColumn column,SchemaColumn schemaColumn)
    {
        return DbTypeHelper.IsHasCorrectType(schemaColumn.Type,column)
               && IsSatisfyConstraints(column, schemaColumn);
    }
    private bool IsSatisfyConstraints(IColumn column,SchemaColumn schemaColumn)
    {
        return schemaColumn.Constraints.All(cons => cons.ApplyConstraint(column));
    }
}