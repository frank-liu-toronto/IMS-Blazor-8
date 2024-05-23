using IMS.CoreBusiness;

namespace IMS.UseCases.Inventories.interfaces
{
    public interface IViewInventoryByIdUseCase
    {
        Task<Inventory> ExecuteAsync(int inventoryId);
    }
}