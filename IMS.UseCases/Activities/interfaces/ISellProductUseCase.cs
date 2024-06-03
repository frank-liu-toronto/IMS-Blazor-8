using IMS.CoreBusiness;

namespace IMS.UseCases.Activities.interfaces
{
    public interface ISellProductUseCase
    {
        Task ExecuteAsync(string salesOrderNumber, Product product, int quantity, string doneBy);
    }
}