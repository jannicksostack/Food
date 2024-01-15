using madtilhjemlose.MVVM.Model.User;
using Microsoft.Data.SqlClient;

namespace madtilhjemlose.MVVM.DataAccess;

public class UserRepository : BaseRepository
{

	public User? TryGetUser(string userName, string password)
	{
		try
		{
			connection.Open();
			using SqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = "select FirmaID, BrugerID, BrugerEmail, BrugerNavn, BrugerHjemmetelefon, BrugerArbejdstelefon, BrugerAdresse, BrugerType.BrugerTypeNavn as BrugerType from Bruger left join BrugerType on BrugerType.BrugerTypeID = Bruger.BrugerTypeID where BrugerNavn = @username and BrugerPassword = @password";
			cmd.Parameters.AddWithValue("@username", userName);
			cmd.Parameters.AddWithValue("@password", password);

			using SqlDataReader reader = cmd.ExecuteReader();
			if (!reader.HasRows)
			{
				return null;
			}

			reader.Read();
			return new User(reader);
		}
		catch (Exception e)
		{
			ShowErrorMessage(e.ToString());
		}
		finally
		{
			connection.Close();
		}
		return null;
	}
}