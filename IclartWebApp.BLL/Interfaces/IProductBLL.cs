using IclartWebApp.Common.Models;

namespace IclartWebApp.BLL
{
    public interface IProductBLL
    {
        void AddProduct(ProductFormModel newProduct);
        void AddProductCategory(string name);
        void DeleteProduct(int id);
        void DeleteProductCategory(int id);
        double GetBestPrice(int clientId, int productId);
        void UpdateProduct(int id, ProductFormModel product);
        void UpdateProductCategory(int id, string name);
    }
}