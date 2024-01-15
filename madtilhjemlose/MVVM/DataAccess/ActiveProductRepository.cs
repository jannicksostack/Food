using madtilhjemlose.MVVM.Model;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace madtilhjemlose.MVVM.DataAccess;

public class ActiveProductRepository : BaseRepository
{
    public ObservableCollection<ActiveProduct> Items { get; set; } = new();

    public event EventHandler<ObservableCollection<ActiveProduct>> RepositoryChanged;

    private List<Product> Products { get; set; }

    public ActiveProductRepository(List<Product> products)
    {
        Products = products;
        GetItems();
    }

    public ActiveProduct? Create(Product product, DateTime date, int quantity, decimal price)
    {
        ActiveProduct? activeProduct = null;
        try
        {
            connection.Open();

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "insert into AktiveProdukt(ProduktID, AktiveProduktUdløbsdato, AktiveProduktMaxMængde, AktiveProduktPris) output Inserted.AktiveProduktID, Inserted.ProduktID, Inserted.AktiveProduktUdløbsdato, Inserted.AktiveProduktMaxMængde, Inserted.AktiveProduktPris values (@1, @2, @3, @4)";
            command.Parameters.AddWithValue("@1", product.Id);
            command.Parameters.AddWithValue("@2", date);
            command.Parameters.AddWithValue("@3", quantity);
            command.Parameters.AddWithValue("@4", price);

            using SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            activeProduct = new(reader, Products);
        }
        catch (Exception e)
        {
            ShowErrorMessage(e.Message);
        }
        finally
        {
            connection.Close();
        }

        GetItems();
        return activeProduct;
    }

    public void Update(ActiveProduct activeProduct)
    {
        try
        {
            connection.Open();
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "update AktiveProdukt set ProduktID = @1, AktiveProduktUdløbsdato = @2, AktiveProduktMaxMængde = @3, AktiveProduktPris = @4 where AktiveProduktID = @5";
            command.Parameters.AddWithValue("@1", activeProduct.Product.Id);
            command.Parameters.AddWithValue("@2", activeProduct.Date);
            command.Parameters.AddWithValue("@3", activeProduct.Quantity);
            command.Parameters.AddWithValue("@4", activeProduct.Price);
            command.Parameters.AddWithValue("@5", activeProduct.Id);

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

        GetItems();
    }

    public void GetItems()
    {
        try
        {
            connection.Open();
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "select AktiveProduktID, ProduktID, AktiveProduktUdløbsdato, AktiveProduktMaxMængde, AktiveProduktPris from AktiveProdukt Where AktiveProduktUdløbsdato >= cast(GetDate() as date)";

            using SqlDataReader reader = command.ExecuteReader();

            List<ActiveProduct> activeProducts = new();
            while (reader.Read())
            {
                activeProducts.Add(new(reader, Products));
            }

            Items = new(activeProducts.OrderBy(x => x.Product.Name).ThenBy(x => x.Date));
            RepositoryChanged?.Invoke(this, Items);
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

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}