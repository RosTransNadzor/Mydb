using DbCore.Database;
using DbCore.Raw;
using DbCore.Schema;
using DbCore.Table;
using DbCore.Table.Constraints;
using DbCore.Table.Operations;

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
                ["Age"] = new SchemaColumn(new ColType{IsNullable = true,Type = DbType.Int32})
                {
                    Constraints = [new AutoIncrementConstraint()]
                },
                ["Name"] = new SchemaColumn(new ColType{IsNullable = true,Type = DbType.String})
                {
                    Constraints = Array.Empty<IConstraint>()
                },
                ["JobID"] = new SchemaColumn(new ColType{IsNullable = true,Type = DbType.Int32},
                    new RowColumn<int?>{Value = 635})
                {
                    Constraints = [new UniqueConstraint()]
                }
            }
        };
        Database db = new Database();
        db.AddTable("Person", schema);
        Table table = db.GetByTableName("Person");
        table.ChangesObservable.Subscribe(to =>
        {
            switch (to.Operation)
            {
                case RowInserted inserted:
                {
                    Console.WriteLine($"Row inserted {inserted.InsertedRow}");
                    break;
                }
                case RowUpdated updated:
                    Console.WriteLine($"Row updated {updated.UpdatedRow} with id {updated.RowId}");
                    break;
                case RowDeleted deleted:
                    Console.WriteLine($"Row deleted with id {deleted.RowId}");
                    break;
            }
        });
        table.InsertValues(new Dictionary<string, IColumn>
        {
            ["Age"] = new RowColumn<int?>{Value = null},
            ["JobID"] = new RowColumn<int?>{Value = 32}
        });
        table.UpdateRow(0,new Dictionary<string, IColumn>
        {
            ["Name"] = new RowColumn<string>{Value = "Fred"}
        });
        table.InsertValues(new Dictionary<string, IColumn>
        {
            ["JobID"] = new RowColumn<int>{Value = 72}
        });
        table.UpdateRow(1,new Dictionary<string, IColumn>
        {
            ["Name"] = new RowColumn<string>{Value = "Mark"}
        });
        table.UpdateRow(1,new Dictionary<string, IColumn>
        {
            ["JobID"] = new RowColumn<int?>{Value = null}
        });
        table.DeleteRow(0);
        table.InsertValues(new Dictionary<string, IColumn>
        {
            ["JobID"] = new RowColumn<int?>{Value = 62}
        });
        table.InsertValues(new Dictionary<string, IColumn>
        {
            ["JobID"] = new RowColumn<int?>{Value = 92}
        });
        table.Print();
    }
}