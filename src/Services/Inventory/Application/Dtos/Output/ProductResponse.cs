namespace Inventory.Application.Dtos.Output
{
    public record ProductResponse
    {
        public string Name { get; init; } = string.Empty;
        public string ProductCategory { get; init; } = string.Empty;
    }
}