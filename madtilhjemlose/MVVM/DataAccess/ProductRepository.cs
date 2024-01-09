using madtilhjemlose.MVVM.Model;

namespace madtilhjemlose.MVVM.DataAccess;

public class ProductRepository : BaseRepository
{
    public ProductRepository() { }

    public Product CreateProduct(string type, string name, string imageUrl)
    {
        throw new NotImplementedException();
    }

    public List<Product> GetProducts()
    {
        throw new NotImplementedException();
    }

    public void UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public void DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }
}