namespace Inventory.Domain.Inventory
{
    public class Inventory
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public Inventory Create(Guid productId, int quantity)
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
            this.Quantity = +quantity;
        }

        public void SetStock(int quantity)
        {
            this.Quantity = quantity;
        }

        public void ReduceStock(int quantity)
        {
            this.Quantity = -quantity;
        }
    }
}