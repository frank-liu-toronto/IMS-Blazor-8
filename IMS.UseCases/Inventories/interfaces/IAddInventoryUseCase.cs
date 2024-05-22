using IMS.CoreBusiness;

namespace IMS.UseCases.Inventories.interfaces
{
    public interface IAddInventoryUseCase
    {
        Task ExecuteAsync(Inventory inventory);
    }
}