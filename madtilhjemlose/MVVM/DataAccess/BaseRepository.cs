using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace madtilhjemlose.MVVM.DataAccess;

public abstract class BaseRepository : IDisposable
{
	protected SqlConnection connection;
    
    public BaseRepository()
	{
		string connectionString = ConfigurationManager.ConnectionStrings["food"].ConnectionString;
		connection = new SqlConnection(connectionString);
	}

    public void Dispose()
    {
		connection.Dispose();
    }
    protected static SqlParameter CreateParam(string name, object value, SqlDbType type)
    {
        SqlParameter param = new(name, type)
        {
            Value = value
        };
        return param;
    }
}