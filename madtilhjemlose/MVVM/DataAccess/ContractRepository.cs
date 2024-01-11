using madtilhjemlose.MVVM.Model;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Data.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace madtilhjemlose.MVVM.DataAccess;

public class ContractRepository : BaseRepository, IEnumerable<Contract>
{
	private readonly List<Contract> list = [];

	public ContractRepository()
	{
	}
    public IEnumerator<Contract> GetEnumerator() // jesper
    {
        return list.GetEnumerator();
    }

    public void Search(string companyName) // Jesper
	{
		// All har adgang til dette, Admin kan vælge firma fra en Picker og normal bruger har hardcodet deres firmaNavn tilsendt
		try
		{
			//get CompanyInfo by Searching the company name
			SqlCommand cmd = new ("SELECT dbo.Kontrakt.KontraktID, dbo.Firma.FirmaNavn, dbo.Firma.FirmaAdresse, " +
				"dbo.Kontrakt.KontraktStartDato, dbo.Kontrakt.KontraktSlutDato, dbo.Kontrakt.KontraktPDF " +
				"FROM dbo.Firma INNER JOIN dbo.Kontrakt ON dbo.Firma.FirmaID = dbo.Kontrakt.FirmaID " +
                "where dbo.Firma.FirmaNavn like @Company;", connection);
			SqlCommand command = cmd;
			command.Parameters.Add(CreateParam("@Company", companyName + "%", SqlDbType.NVarChar));
			connection.Open();
			SqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				list.Add(new Contract(Int32.Parse(reader[0].ToString()),
					reader[1].ToString(),
					reader[2].ToString(),
					reader[3].ToString(),
					reader[4].ToString()
					));
			}

		}
		catch (Exception ex)
		{
			// throw an error message
			throw;
        }
		finally 
		{
            // close connection
            if (connection != null && connection.State == ConnectionState.Open) connection.Close();
        }
	}

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}