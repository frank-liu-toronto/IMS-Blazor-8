using IMS.CoreBusiness;

namespace IMS.UseCases.Products.interfaces
{
    public interface IViewProductByIdUseCase
    {
        Task<Product?> ExecuteAsync(int productId);
    }
}