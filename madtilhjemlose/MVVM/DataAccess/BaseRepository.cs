using Microsoft.Data.SqlClient;
using System.Configuration;

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
}