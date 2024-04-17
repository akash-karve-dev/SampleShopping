namespace Inventory.Application.Dtos.Output
{
    public record InventoryResponse
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}