using IMS.CoreBusiness;

namespace IMS.UseCases.Inventories.interfaces
{
    public interface IViewInventoriesByNameUseCase
    {
        Task<IEnumerable<Inventory>> ExecuteAsync(string name = "");
    }
}