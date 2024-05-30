using IMS.CoreBusiness;

namespace IMS.UseCases.Activities.interfaces
{
    public interface IPurchaseInventoryUseCase
    {
        Task ExecuteAsync(string poNumber, Inventory inventory, int quantity, string doneBy);
    }
}