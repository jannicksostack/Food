using madtilhjemlose.MVVM.Model;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;

namespace madtilhjemlose.MVVM.DataAccess;

public class CompanyRepository : BaseRepository
{
    public ObservableCollection<Company> Companies { get; set; } = new();
        public CompanyRepository() { }

    public event EventHandler<ObservableCollection<Company>> RepositoryChanged;
    
    public Company CreateCompany(string name, string address)
    {
        Company? company = null;
        try
        {
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Firma(FirmaNavn, FirmaAdresse) otuput Inserted.FirmaID, Inserted.FirmaNavn, Inserted.FirmaAdresse values (@1,@2)";
            command.Parameters.AddWithValue("@1", name);
            command.Parameters.AddWithValue("@2", address);

            using SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            company = new(reader);

            
        }
        catch (Exception e)
        {
            ShowErrorMessage(e. Message);

        }
            finally
        {  connection.Close(); }
        
            GetCompanies();
            return company;
               
    }

    public void UpdateCompany(Company company)
    {
        try
        {
            connection.Open();
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE Firma set FirmaNavn = @1, FirmaAdresse = @2 WHERE FirmaID = @3";
            command.Parameters.AddWithValue("@1", company.Name);
            command.Parameters.AddWithValue("@2", company.Address);
            command.Parameters.AddWithValue("@3", company.Id);

            command.ExecuteNonQuery();
        }
        catch (Exception e) { ShowErrorMessage(e.ToString()); }
        finally { connection.Close(); }

        GetCompanies();
    }
    public void GetCompanies()
    {
        try
        {
            connection.Open();
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT FirmaID, FirmaNavn, FirmaAdresse FROM Firma";

            using SqlDataReader reader = command.ExecuteReader();

            List<Company> companies = new();
            while (reader.Read())
            {
               companies.Add(new Company(reader));
            }
            Companies = new(companies);
            RepositoryChanged.Invoke(this, Companies);
        }
        catch (Exception e) {ShowErrorMessage(e.ToString());}
        finally { connection.Close(); }
    }
    
}
