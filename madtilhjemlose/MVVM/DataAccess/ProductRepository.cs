using madtilhjemlose.MVVM.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace madtilhjemlose.MVVM.DataAccess;

public class ProductRepository : BaseRepository
{
    public ProductRepository() { }

    public bool CreateProduct(string type, string name, string imagePath)
    {
        try
        {
            connection.Open();

            byte[] imageData = File.ReadAllBytes(imagePath);
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
            return false;
        }
        finally
        {
            connection.Close();
        }
        return true;
    }

    public List<Product> GetProducts()
    {
        try
        {
            connection.Open();
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "select ProduktID, ProduktNavn, ProduktType, ProduktBillede from Produkt order by ProduktType, ProduktNavn";

            using SqlDataReader reader = command.ExecuteReader();

            List<Product> products = new();
            while (reader.Read())
            {
                int id = (int) reader["ProduktID"];
                string name = (string) reader["ProduktNavn"];
                string type = (string) reader["ProduktType"];
                byte[]? imageData = reader.IsDBNull("ProduktBillede") ? null : (byte[]) reader["ProduktBillede"];

                Product p = new(id, type, name, imageData);

                products.Add(p);
            }
            return products;
        }
        catch(Exception e)
        {
            ShowErrorMessage(e.ToString());
        }
        finally
        {
            connection.Close();
        }
        return new List<Product>();
    }

    public void UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public void DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }
}