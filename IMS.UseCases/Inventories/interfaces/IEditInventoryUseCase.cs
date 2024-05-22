using IMS.CoreBusiness;

namespace IMS.UseCases.Inventories.interfaces
{
    public interface IEditInventoryUseCase
    {
        Task ExecuteAsync(Inventory inventory);
    }
}