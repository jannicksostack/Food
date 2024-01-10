using CommunityToolkit.Mvvm.ComponentModel;

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
    public static byte[] GetImageData(string imagePath)
    {
        return File.ReadAllBytes(imagePath);
    }
}