using madtilhjemlose.MVVM.Model;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace madtilhjemlose.MVVM.DataAccess;

public class ProductRepository : BaseRepository
{
    public ObservableCollection<Product> Products { get; set; } = new();
    public ProductRepository() {
    }

    public event EventHandler<ObservableCollection<Product>> RepositoryChanged;

    public Product? CreateProduct(string type, string name, byte[]? imageData)
    {
        Product? product = null;
        try
        {
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "insert into Produkt(ProduktType, ProduktNavn, ProduktBillede) output Inserted.ProduktID, Inserted.ProduktNavn, Inserted.ProduktType, Inserted.ProduktBillede values (@1, @2, @3)";
            command.Parameters.AddWithValue("@1", type);
            command.Parameters.AddWithValue("@2", name);
            command.Parameters.AddWithValue("@3", imageData);

            using SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            product = new(reader);
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
        return product;
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
            command.Parameters.AddWithValue("@4", product.Id);

            command.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            ShowErrorMessage(e.ToString());
        }
        finally
        {
            connection.Close();
        }

        GetProducts();
    }

    public ObservableCollection<Product> GetProducts()
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
                products.Add(new Product(reader));
            }

            Products = new(products);
            RepositoryChanged?.Invoke(this, Products);
            return Products;
        }
        catch(Exception e)
        {
            ShowErrorMessage(e.ToString());
        }
        finally
        {
            connection.Close();
        }
        return new();
    }

    public void DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }
}