namespace madtilhjemlose.MVVM.Model;

public class Product
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Type { get; set; }
	public string ImagePath { get; set; }

	public Product(int id, string type, string name, string imagePath)
	{
		Id = id;
		Type = type;
		Name = name;
		ImagePath = imagePath;
	}
}