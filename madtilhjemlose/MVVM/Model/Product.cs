using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Data.SqlClient;
using System.Data;

namespace madtilhjemlose.MVVM.Model;

public partial class Product : ObservableObject
{
	[ObservableProperty]
    private int id;

	[ObservableProperty]
	private string name;

	[ObservableProperty]
	private string type;

	[ObservableProperty]
	private ImageSource? imageSource;

	[ObservableProperty]
	private byte[]? imageData;

    partial void OnImageDataChanged(byte[]? oldValue, byte[]? newValue)
    {
		if (newValue == oldValue)
		{
			return;
		}

		if (newValue is null)
		{
			ImageSource = null;
		} else
		{
            BinaryData data = new BinaryData(newValue);
            ImageSource = ImageSource.FromStream(() => data.ToStream());
        }
    }

    public Product(int id, string type, string name, byte[]? imageData)
	{
		Id = id;
		Type = type;
		Name = name;
		ImageData = imageData;

	}

	public Product(SqlDataReader reader)
	{
        Id = (int) reader["ProduktID"];
        Name = (string) reader["ProduktNavn"];
        Type = (string) reader["ProduktType"];
        ImageData = reader.IsDBNull("ProduktBillede") ? null : (byte[]) reader["ProduktBillede"];
    }
    public static byte[] GetImageData(string imagePath)
    {
        return File.ReadAllBytes(imagePath);
    }
}