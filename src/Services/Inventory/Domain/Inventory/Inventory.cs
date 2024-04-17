namespace Inventory.Domain.Inventory
{
    public class Inventory : IAuditableEntity
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public static Inventory Create(Guid productId, int quantity)
        {
            var inventory = new Inventory()
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                Quantity = quantity
            };

            return inventory;
        }

        public void AddStock(int quantity)
        {
            Quantity += quantity;
        }

        public void SetStock(int quantity)
        {
            Quantity = quantity;
        }

        public void ReduceStock(int quantity)
        {
            Quantity -= quantity;
        }
    }
}