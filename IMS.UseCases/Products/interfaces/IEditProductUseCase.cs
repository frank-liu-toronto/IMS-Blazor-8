using IMS.CoreBusiness;

namespace IMS.UseCases.Products.interfaces
{
    public interface IEditProductUseCase
    {
        Task ExecuteAsync(Product product);
    }
}