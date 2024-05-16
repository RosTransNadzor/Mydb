using DbCore.Database;
using DbCore.Raw;
using DbCore.Schema;
using DbCore.Table.Constraints;
using DbCore.Table.Operations;
using OneOf.Types;

namespace DbCore.Table;

public class RowValidator
{
    private readonly TableManager Manager;
    public RowValidator(TableManager manager)
    {
        Manager = manager;
    }
    public RowValidationResult ValidateRow(RowValidationInfo rowValidationInfo)
    {
        Row row = rowValidationInfo.Row;
        Schema schema = rowValidationInfo.Schema;
        if(!row.Columns
               .Select((col,index) => (col,index))
               .Zip(schema.Columns.Values)
               .All(t => ValidateCollumn
                   (t.First.col,t.Second,rowValidationInfo.Operation,t.First.index))
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
    private bool ValidateCollumn
        (IColumn column,SchemaColumn schemaColumn,IOperation operation,int columnId)
    {
        return DbTypeHelper.IsHasCorrectType(schemaColumn.Type,column)
               && IsSatisfyConstraints(column, schemaColumn,operation,columnId);
    }
    private bool IsSatisfyConstraints
        (IColumn column,SchemaColumn schemaColumn,IOperation operation,int colId)
    {
        return schemaColumn.Constraints
            .All(cons => cons.ApplyConstraint(new ApplyConstrantInfo
            {
                ColId = colId,
                Column = column,
                Manager = Manager,
                OperationType = operation
            }));
    }
}