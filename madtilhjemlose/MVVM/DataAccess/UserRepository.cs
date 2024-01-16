using madtilhjemlose.MVVM.Model.User;
using Microsoft.Data.SqlClient;
using System.Data;

namespace madtilhjemlose.MVVM.DataAccess;

public class UserRepository : BaseRepository
{

	public UserRepository()
	{
	}

	public void CreateUser(string companyName, int brugerTypeID, string brugerNavn, string brugerPassword) // jesper - creates a user in db
	{
		try
		{
            SqlCommand cmd = new("BEGIN TRANSACTION; " +
                "select dbo.Firma.FirmaID from dbo.Firma where FirmaNavn like @CompanyName" +
                "DECLARE @@LastIDFirma INT; SET @@LastIDFirma = SCOPE_IDENTITY();" + // gets the last identify value generated during the insert
                "INSERT INTO dbo.Bruger(FirmaID, BrugerTypeID, BrugerNavn, BrugerPassword) " +
				"VALUES (@LastIDFirma,@brugerTypeID, @brugerNavn, @brugerPWD); " +
                "COMMIT;", connection);
            SqlCommand command = cmd;
            command.Parameters.Add(CreateParam("@CompanyName", companyName + "%", SqlDbType.NVarChar));
			command.Parameters.Add(CreateParam("@brugerTypeID", brugerTypeID, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@brugerNavn", brugerNavn, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@brugerPWD", brugerPassword, SqlDbType.NVarChar));
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
        }
		catch (Exception ex)
		{

			throw;
		}
		finally { if (connection != null && connection.State == ConnectionState.Open) connection.Close(); }

    }

	public void UpdateUser() // jesper
	{

	}
	public void DelteUser() // jesper
	{

	}

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