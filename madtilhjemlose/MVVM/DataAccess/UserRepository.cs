using madtilhjemlose.MVVM.Model.User;
using Microsoft.Data.SqlClient;

namespace madtilhjemlose.MVVM.DataAccess;

public class UserRepository : BaseRepository
{

	public UserRepository()
	{
	}

	public BaseUser? TryGetUser(string userName, string password)
	{
		try
		{
			connection.Open();
			using SqlCommand cmd = connection.CreateCommand();
			cmd.CommandText = "select FirmaID, BrugerID, BrugerEmail, BrugerNavn, BrugerType.BrugerTypeNavn as BrugerType from Bruger left join BrugerType on BrugerType.BrugerTypeID = Bruger.BrugerTypeID where BrugerNavn = @username and BrugerPassword = @password";
			cmd.Parameters.AddWithValue("@username", userName);
			cmd.Parameters.AddWithValue("@password", password);

			using SqlDataReader reader = cmd.ExecuteReader();
			if (!reader.HasRows)
			{
				return null;
			}

			reader.Read();
			UserType type = reader["BrugerType"].ToString()!.ToUserType();
			return type switch
			{
				UserType.Admin => AdminUser.FromReader(reader),
				UserType.Default => DefaultUser.FromReader(reader),
				UserType.Restricted => RestrictedUser.FromReader(reader),
				_ => throw new InvalidDataException()
			};
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