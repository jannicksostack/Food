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
			cmd.CommandText = "select UserID, UserEmail, UserType.Value as UserType from Users left join UserType on UserType.ID = Users.UserType where UserName = @username and UserPassword = @password";
			cmd.Parameters.AddWithValue("@username", userName);
			cmd.Parameters.AddWithValue("@password", password);

			using SqlDataReader reader = cmd.ExecuteReader();
			if (!reader.HasRows)
			{
				return null;
			}

			reader.Read();
			UserType type = reader["UserType"].ToString()!.ToUserType();
			return type switch
			{
				UserType.Admin => AdminUser.FromReader(reader),
				UserType.Default => DefaultUser.FromReader(reader),
				UserType.Restricted => RestrictedUser.FromReader(reader),
				_ => throw new InvalidDataException()
			};
		} catch (Exception e)
		{
			Console.WriteLine("Exception:");
			Console.WriteLine(e.Message);
		} finally
		{
			connection.Close();
		}
		return null;
	}
}