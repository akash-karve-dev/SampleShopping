namespace Inventory.Domain.Product
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ProductCategory { get; set; } = string.Empty;

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