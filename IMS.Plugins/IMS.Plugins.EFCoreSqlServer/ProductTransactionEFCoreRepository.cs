using IMS.CoreBusiness;
using IMS.Plugins.EFCoreSqlServer;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Plugins.InMemory
{
    public class ProductTransactionEFCoreRepository : IProductTransactionRepository
    {
        private readonly IDbContextFactory<IMSContext> contextFactory;
        private readonly IProductRepository productRepository;
        private readonly IInventoryTransactionRepository inventoryTransactionRepository;
        private readonly IInventoryRepository inventoryRepository;

        public ProductTransactionEFCoreRepository(IDbContextFactory<IMSContext> contextFactory,
            IProductRepository productRepository,
            IInventoryTransactionRepository inventoryTransactionRepository,
            IInventoryRepository inventoryRepository)
        {
            this.contextFactory = contextFactory;
            this.productRepository = productRepository;
            this.inventoryTransactionRepository = inventoryTransactionRepository;
            this.inventoryRepository = inventoryRepository;
        }

        public async Task ProduceAsync(string productionNumber, Product product, int quantity, string doneBy)
        {
            using var db = contextFactory.CreateDbContext();

            //decrease the inventories
            var prod = await this.productRepository.GetProductByIdAsync(product.ProductId);
            if (prod is not null && prod.ProductInventories is not null && prod.ProductInventories.Count > 0)
            {
                foreach(var pi in prod.ProductInventories)
                {
                    if (pi.Inventory is not null)
                    {
                        //add inventory transaction
                        await this.inventoryTransactionRepository.ProduceAsync(productionNumber,
                            pi.Inventory,
                            pi.InventoryQuantity * quantity,
                            doneBy,
                            -1);

                        //decrease the inventories
                        var inv = await this.inventoryRepository.GetInventoryByIdAsync(pi.InventoryId);
                        inv.Quantity -= pi.InventoryQuantity * quantity;
                        await this.inventoryRepository.UpdateInventoryAsync(inv);
                    }                    
                }
            }

            //add product transaction
            db.ProductTransactions?.Add(new ProductTransaction
            {
                ProdutionNumber = productionNumber,
                ProductId = product.ProductId,
                QuantityBefore = product.Quantity,
                ActivityType = ProductTransactionType.ProduceProduct,
                QuantityAfter = product.Quantity + quantity,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy
            });

            await db.SaveChangesAsync();
        }

        public async Task SellProductAsync(string salesOrderNumber, Product product, int quantity, double unitPrice, string doneBy)
        {
            using var db = contextFactory.CreateDbContext();

            db.ProductTransactions?.Add(new ProductTransaction
            {
                ActivityType = ProductTransactionType.SellProduct,
                SONumber = salesOrderNumber,
                ProductId = product.ProductId,
                QuantityBefore = product.Quantity,
                QuantityAfter = product.Quantity + quantity,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy,
                UnitPrice = unitPrice
            });

            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductTransaction>> GetProductTransactionAsync(string productName, DateTime? dateFrom, DateTime? dateTo, ProductTransactionType? transactionType)
        {
            using var db = contextFactory.CreateDbContext();

            var query = from it in db.ProductTransactions
                        join inv in db.Products on it.ProductId equals inv.ProductId
                        where
                            (string.IsNullOrWhiteSpace(productName) || inv.ProductName.ToLower().IndexOf(productName.ToLower()) >= 0)
                            &&
                            (!dateFrom.HasValue || it.TransactionDate >= dateFrom.Value.Date) &&
                            (!dateTo.HasValue || it.TransactionDate <= dateTo.Value.Date) &&
                            (!transactionType.HasValue || it.ActivityType == transactionType)
                        select it;

            return await query.Include(x => x.Product).ToListAsync();
        }
    }
}
