using IMS.CoreBusiness;

namespace IMS.UseCases.Products.interfaces
{
    public interface IAddProductUseCase
    {
        Task ExecuteAsync(Product product);
    }
}