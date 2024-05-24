using IMS.CoreBusiness;

namespace IMS.UseCases.Products.interfaces
{
    public interface IViewProductsByNameUseCase
    {
        Task<IEnumerable<Product>> ExecuteAsync(string name = "");
    }
}