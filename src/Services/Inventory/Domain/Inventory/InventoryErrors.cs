using SharedKernel.ResultPattern;

namespace Inventory.Domain.Inventory
{
    public class InventoryErrors
    {
        public static Error NotFound(Guid id) => new("Inventory.NotFound", $"Inventory not found for Id: [{id}]");
    }
}