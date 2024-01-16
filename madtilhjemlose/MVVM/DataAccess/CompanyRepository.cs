using madtilhjemlose.MVVM.Model;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace madtilhjemlose.MVVM.DataAccess;

public class CompanyRepository : BaseRepository, IEnumerable<Company>
{
    private readonly List<Company> list = [];

    public ObservableCollection<Company> Companies { get; set; } = new();
    public CompanyRepository() { }

    public event EventHandler<ObservableCollection<Company>> RepositoryChanged;

    public IEnumerator<Company> GetEnumerator ()
    { return list.GetEnumerator(); }
    
    public Company CreateCompany(string name, string address)
    {
        Company? company = null;
        try
        {
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Firma(FirmaNavn, FirmaAdresse) output Inserted.FirmaID, Inserted.FirmaNavn, Inserted.FirmaAdresse values (@1,@2)";
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

    public void SearchCompany(string companyName)
        //cerca nel DB le aziende che hanno un nome simile a quello fornito come parametro
    {
        try
        {
            SqlCommand cmd = new("SELECT FirmaID, FirmaNavn, FirmaAdresse FROM Firma WHERE FirmaNavn LIKE @FirmaNavn ", connection);
            SqlCommand command = cmd;
            command.Parameters.Add(CreateParam("@Name", companyName + "%", SqlDbType.NVarChar));
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            list.Clear();
            while (reader.Read()) list.Add(new Company(Int32.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString()));
            
        }
        catch (Exception e)
        { 
            ShowErrorMessage(e.ToString()); 
        }
    
        finally
        {
            if (connection != null && connection.State == ConnectionState.Open) connection.Close();
        }
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
        //tiene aggioranta la lista delle aziende dopo la creazione o l'update
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
            //invoca la repository per avvisare che la lista delle aziende é cambiata
        }
        catch (Exception e) {ShowErrorMessage(e.ToString());}
        finally { connection.Close(); }
    }
    IEnumerator IEnumerable.GetEnumerator() 
    {
        throw new NotImplementedException();
    }
    
}
