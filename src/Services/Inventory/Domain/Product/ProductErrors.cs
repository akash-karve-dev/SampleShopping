using SharedKernel.ResultPattern;

namespace Inventory.Domain.Product
{
    public class ProductErrors
    {
        public static Error NotFound(Guid productId) => new("Product.NotFound", $"Product not found for Id: [{productId}]");
    }
}