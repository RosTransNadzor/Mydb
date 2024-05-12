
namespace DbCore.Database;
using Table;
public class Database
{
    private Dictionary<string,Table> tables = [];
    public Database() {}
    public bool AddTable(string tableName,Schema tableSchema)
    {
        RowValidator validator = new RowValidator(new TableManager(this));
        Table t = new Table(validator)
        {
            Schema = tableSchema
        };
        if (!tables.TryAdd(tableName, t))
            return false;
        return true;
    }

    public Table? GetByTableName(string name)
    {
        tables.TryGetValue(name, out Table? table);
        return table;
    }
}