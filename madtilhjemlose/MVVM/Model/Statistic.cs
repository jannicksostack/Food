using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Data.SqlClient;

namespace madtilhjemlose.MVVM.Model;

public partial class Statistic : ObservableObject
{

	[ObservableProperty]
	private int prodId;
    [ObservableProperty]
    private string prodType;
    [ObservableProperty]
    private string prodName;
    [ObservableProperty]
    private int custId;
    [ObservableProperty]
    private string custName;
    [ObservableProperty]
    private double monthPro;
    [ObservableProperty]
    private double yearPro;

    public Statistic(int prodId, string prodType, string prodName, int custId, string custName, double monthPro, double yearPro)
	{
        ProdId = prodId;
        ProdType = prodType;
        ProdName = prodName;
        CustId = custId;
        CustName = custName;
        MonthPro = monthPro;
        YearPro = yearPro;
        		
	}
    public Statistic(SqlDataReader reader) 
    {
        ProdId = (int)reader["ProduktID"];
        ProdType = (string)reader["ProduktType"];
        ProdName = (string)reader["ProduktNavn"];
        CustId = (int)reader["FirmaID"];
        CustName = (string)reader["FirmaNavn"];
        MonthPro = (double)reader[""];
        YearPro = (double)reader[""];
        
    }
}