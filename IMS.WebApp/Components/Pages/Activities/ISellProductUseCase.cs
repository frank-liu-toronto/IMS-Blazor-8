using IMS.CoreBusiness;

namespace IMS.WebApp.Components.Pages.Activities
{
    public interface ISellProductUseCase
    {
        Task ExecuteAsync(string salesOrderNumber, Product product, int quantity, string doneBy);
    }
}