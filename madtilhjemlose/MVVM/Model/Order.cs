using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Data.SqlClient;
using System.Data;

namespace madtilhjemlose.MVVM.Model;

public partial class Order : ObservableObject
{
	[ObservableProperty]
	private int id;

	[ObservableProperty]
	private int userId;

	[ObservableProperty]
	private DateOnly date;

	[ObservableProperty]
	private bool active;
	public Order(SqlDataReader reader)
	{
		Id = (int) reader["OrdreID"];
		UserId = (int) reader["BrugerID"];
		Date = DateOnly.FromDateTime((DateTime) reader["OrdreDato"]);
		Active = reader.IsDBNull("Aktiv") ? false : (bool) reader["Aktiv"];
	}
}