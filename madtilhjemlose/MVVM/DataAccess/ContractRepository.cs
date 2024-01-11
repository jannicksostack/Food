using Microsoft.Data.SqlClient;
using System.Data;

namespace madtilhjemlose.MVVM.DataAccess;

public class ContractRepository : BaseRepository
{
	
	public ContractRepository()
	{
	}
	public void Search(string companyName)
	{
		// All har adgang til dette, Admin kan vælge firma fra en Picker og normal bruger har hardcodet deres firmaNavn tilsendt
		try
		{
			//get CompanyInfo by Searching the company name
			SqlCommand cmd = new ("SELECT dbo.Kontrakt.KontraktID, dbo.Firma.FirmaNavn, dbo.Firma.FirmaAdresse, " +
				"dbo.Kontrakt.KontraktStartDato, dbo.Kontrakt.KontraktSlutDato, dbo.Kontrakt.KontraktPDF " +
				"FROM dbo.Firma INNER JOIN dbo.Kontrakt ON dbo.Firma.FirmaID = dbo.Kontrakt.FirmaID " +
                "where dbo.Firma.FirmaNavn = @Company;", connection);
			SqlCommand command = cmd;
			command.Parameters.Add(CreateParam("@Company", companyName + "%", SqlDbType.NVarChar));
			connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{

			}

		}
		catch (Exception ex)
		{
			// throw an DbExceptionError
			throw;
		}
		finally 
		{
			// close connection
		}
	}
}