using IMS.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.PluginInterfaces
{
    public interface IInventoryRepository
    {
        Task AddInventoryAsync(Inventory inventory);
        Task DeleteInventoryByIdAsync(int inventoryId);
        Task<IEnumerable<Inventory>> GetInventoriesByNameAsync(string name);
        Task<Inventory> GetInventoryByIdAsync(int inventoryId);
        Task UpdateInventoryAsync(Inventory inventory);
    }
}
