using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CoreBusiness.Validations
{
    public class Product_EnsurePriceGreaterThanInventoriesCost : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var product = validationContext.ObjectInstance as Product;
            if (product != null)
            {
                if (!ValidatePricing(product))
                    return new ValidationResult($"The product's price is less than the inventories cost: {TotalInventoriesCost(product).ToString("c")}!",
                        new List<string>() { validationContext.MemberName });
            }

            return ValidationResult.Success;
        }

        private double TotalInventoriesCost(Product product)
        {
            if (product is null || product.ProductInventories is null) return 0;

            return product.ProductInventories.Sum(x => x.Inventory?.Price * x.InventoryQuantity ?? 0);
        }

        private bool ValidatePricing(Product product)
        {
            if (product is null || 
                product.ProductInventories is null ||  
                product.ProductInventories.Count <= 0) return true;

            if (TotalInventoriesCost(product) > product.Price) return false;

            return true;
        }


    }
}
