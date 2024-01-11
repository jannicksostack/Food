namespace madtilhjemlose.MVVM.Model;

public class Contract : ContentPage
{
	public int ContractID {  get; set; }
	public string ContractStart {  get; set; }
	public string ContractEnd {  get; set; }
	public string CompanyName {  get; set; }
	public string CompanyAddress { get; set; }



	public Contract()
	{
		ContractID = 0;
		ContractStart = "";
		ContractEnd = "";
		CompanyName = "";
		CompanyAddress = "";
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				}
			}
		};
	}
	public Contract(int contractID, string companyName, string contractStart, string contractEnd, string companyAddress)
    {
        ContractID = contractID;
        ContractStart = contractStart;
        ContractEnd = contractEnd;
        CompanyName = companyName;
        CompanyAddress = companyAddress;
    }
}