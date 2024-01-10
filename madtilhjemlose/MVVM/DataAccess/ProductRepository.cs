using madtilhjemlose.MVVM.Model;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace madtilhjemlose.MVVM.DataAccess;

public class ProductRepository : BaseRepository
{
    public ObservableCollection<Product> Products { get; set; } = new();
    public ProductRepository() {
        GetProducts();
    }


    public void CreateProduct(string type, string name, byte[]? imageData)
    {
        try
        {
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "insert into Produkt(ProduktType, ProduktNavn, ProduktBillede) values (@1, @2, @3)";
            command.Parameters.AddWithValue("@1", type);
            command.Parameters.AddWithValue("@2", name);
            command.Parameters.AddWithValue("@3", imageData);

            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            ShowErrorMessage(e.Message);
        }
        finally
        {
            connection.Close();
        }
        GetProducts();
    }

    public void UpdateProduct(Product product)
    {
        try
        {
            connection.Open();
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "update Produkt set ProduktNavn = @1, ProduktType = @2, ProduktBillede = @3 where ProduktID = @4";
            command.Parameters.AddWithValue("@1", product.Name);
            command.Parameters.AddWithValue("@2", product.Type);
            command.Parameters.AddWithValue("@3", product.ImageData);
            command.Parameters.AddWithValue("@4", product.Name);
        }
        catch (Exception e)
        {
            ShowErrorMessage(e.ToString());
        }
        finally
        {
            connection.Close();
        }
    }

    public void GetProducts()
    {
        try
        {
            connection.Open();
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "select ProduktID, ProduktNavn, ProduktType, ProduktBillede from Produkt order by ProduktType, ProduktNavn";

            using SqlDataReader reader = command.ExecuteReader();

            Products.Clear();
            while (reader.Read())
            {
                int id = (int) reader["ProduktID"];
                string name = (string) reader["ProduktNavn"];
                string type = (string) reader["ProduktType"];
                byte[]? imageData = reader.IsDBNull("ProduktBillede") ? null : (byte[]) reader["ProduktBillede"];

                Product p = new(id, type, name, imageData);

                Products.Add(p);
            }
        }
        catch(Exception e)
        {
            ShowErrorMessage(e.ToString());
        }
        finally
        {
            connection.Close();
        }
    }

    public void DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }
}