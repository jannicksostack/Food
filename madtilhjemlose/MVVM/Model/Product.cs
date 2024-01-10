namespace madtilhjemlose.MVVM.Model;

public class Product
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Type { get; set; }
	public ImageSource? ImageSource { get; set; }

	public Product(int id, string type, string name, byte[]? imageData)
	{
		Id = id;
		Type = type;
		Name = name;

		if (imageData is null)
		{
			return;
		}

		BinaryData data = new BinaryData(imageData);

		ImageSource = ImageSource.FromStream(() => data.ToStream());
	}
}