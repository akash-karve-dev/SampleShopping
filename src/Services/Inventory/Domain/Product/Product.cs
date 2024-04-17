namespace Inventory.Domain.Product
{
    public class Product : IAuditableEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ProductCategory { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public static Product Create(string productName, string productCategory)
        {
            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = productName,
                ProductCategory = productCategory
            };

            return product;
        }
    }
}