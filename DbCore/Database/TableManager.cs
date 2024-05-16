namespace DbCore.Database;
using Table;
public class TableManager
{
    private readonly Database _database;
    private Table? Current;
    
    public TableManager(Database databese)
    {
        _database = databese;
    }

    public void SetCurrent(Table current)
    {
        Current = current;
    }

    public Table GetTableByName(string name)
    {
        if (name == "current" && Current is not null)
            return Current;
        
        return _database.GetByTableName(name);
    }
}