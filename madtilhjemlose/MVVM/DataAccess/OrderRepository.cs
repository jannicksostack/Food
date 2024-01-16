using madtilhjemlose.MVVM.Model;
using madtilhjemlose.MVVM.Model.User;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Transactions;

namespace madtilhjemlose.MVVM.DataAccess;

public class OrderRepository : BaseRepository
{

    public EventHandler<ObservableCollection<Order>> OrdersChanged;
    public ObservableCollection<Order> Orders;

    public EventHandler<ObservableCollection<OrderDetails>> OrderDetailsChanged;
    public ObservableCollection<OrderDetails> OrderDetails;
	public OrderRepository()
	{
        GetOrders();
        GetOrderDetails();
	}

    private void GetOrderDetails()
    {
        try
        {
            connection.Open();
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "select OrdreID, AktiveProduktID, Mængde, TotalBeløb from OrdreDetaljer";

            using SqlDataReader reader = command.ExecuteReader();

            List<OrderDetails> list = new();
            while (reader.Read())
            {
                list.Add(new(reader));
            }

            OrderDetails = new(list);
            OrderDetailsChanged?.Invoke(this, OrderDetails);
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
    private void GetOrders()
    {
        try
        {
            connection.Open();
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "select OrdreID, BrugerID, OrdreDato, Aktiv from Ordre";

            using SqlDataReader reader = command.ExecuteReader();

            List<Order> list = new();
            while (reader.Read())
            {
                list.Add(new(reader));
            }

            Orders = new(list);
            OrdersChanged?.Invoke(this, Orders);
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

    public void PlaceOrder(ShoppingCart cart)
    {
        try
        {
            connection.Open();
            using SqlTransaction transaction = connection.BeginTransaction();
            using SqlCommand command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = "insert into Ordre(BrugerID, OrdreDato, Aktiv) output inserted.OrdreID values (@1, @2, @3)";
            command.Parameters.AddWithValue("@1", User.Current.Id);
            command.Parameters.AddWithValue("@2", DateOnly.FromDateTime(DateTime.Now));
            command.Parameters.AddWithValue("@3", 1);

            using SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            int orderId = (int) reader["OrdreID"];

            reader.Close();

            foreach (ShoppingCartItem item in cart.Items)
            {
                command.CommandText = "insert into OrdreDetaljer (OrdreID, AktiveProduktID, Mængde, TotalBeløb) values (@1, @2, @3, @4)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@1", orderId);
                command.Parameters.AddWithValue("@2", item.ActiveProduct.Id);
                command.Parameters.AddWithValue("@3", item.Quantity);
                command.Parameters.AddWithValue("@4", item.Total);

                command.ExecuteNonQuery();
            }

            transaction.Commit();
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

}