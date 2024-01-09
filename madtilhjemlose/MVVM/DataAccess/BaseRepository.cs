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

	protected void ShowErrorMessage(string message)
	{
		string title = "Repository Error";
		App.Current.MainPage.Navigation.NavigationStack.Last().DisplayAlert(title, message, "Ok");
	}
}