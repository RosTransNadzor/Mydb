using DbCore.Database;
using DbCore.Raw;
using DbCore.Schema;
using DbCore.Table;
using DbCore.Table.Constraints;

namespace Cons;

static class Program
{
    public static void Main(string[] args)
    {
        Row row = new Row
        {
            Columns = new IColumn[]
            {
                new RowColumn<int>{Value = 3},
                new RowColumn<string>{Value = "Mark"},
                new RowColumn<int>{Value = 45}
            }
        };
        Schema schema = new Schema
        {
            Columns = new Dictionary<string, SchemaColumn>
            {
                ["Age"] = new SchemaColumn(new ColType{IsNullable = false,Type = DbType.Int32})
                {
                    Constraints = Array.Empty<IConstraint>()
                },
                ["Name"] = new SchemaColumn(new ColType{IsNullable = false,Type = DbType.String})
                {
                    Constraints = Array.Empty<IConstraint>()
                },
                ["JobID"] = new SchemaColumn(new ColType{IsNullable = true,Type = DbType.Int32},new RowColumn<int>{Value = 635})
                {
                    Constraints = Array.Empty<IConstraint>()
                }
            }
        };
        Database db = new Database();
        Table table = new Table(new RowValidator(new TableManager(db)))
        {
            Schema = schema
        };
        table.InsertValues(new Dictionary<string, IColumn>
        {
            ["Age"] = new RowColumn<int>{Value = 5}
        });
        table.UpdateRow(0,new Dictionary<string, IColumn>
        {
            ["Age"] = new RowColumn<int?>{Value = null},
            ["Name"] = new RowColumn<string>{Value = "Fred"}
        });
        Console.WriteLine("end");
    }
}