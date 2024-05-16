
namespace DbCore.Database;
using Table;
public class Database
{
    private Dictionary<string,Table> tables = [];
    public Database() {}
    public bool AddTable(string tableName,Schema tableSchema)
    {
        TableManager manager = new TableManager(this);
        Table t = new Table(manager)
        {
            Schema = tableSchema
        };
        if (!tables.TryAdd(tableName, t))
            return false;
        return true;
    }
    
    public Table GetByTableName(string name)
    {
        return tables[name];
    }
}