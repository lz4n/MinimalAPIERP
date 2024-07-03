namespace BlazorApp.Dtos
{
    public class OrderViewDto
    {
        public Guid? Guid { get; set; }
        public DateTime OrderDate { get; set; }
        public string Username { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public decimal Total { get; set; }
    }

    public class CategoryViewDto
    {
        public Guid? Guid { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class RaincheckViewDto
    {
        public Guid? Guid { get; set; }
        public string? Name { get; set; }
        public int Count { get; set; }
        public double SalePrice { get; set; }
        public StoreViewDto? Store { get; set; }
        public ProductViewDto? Product { get; set; }
    }

    public class StoreViewDto
    {
        public Guid? Guid { get; set; }
        public string? Name { get; set; }
    }

    public class ProductViewDto
    {
        public Guid? Guid { get; set; }
        public string SkuNumber { get; set; }
        public int RecommendationId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public string? ProductArtUrl { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string ProductDetails { get; set; }
        public int Inventory { get; set; }
        public int LeadTime { get; set; }
        public CategoryViewDto? Category { get; set; }
    }

    public class CartItemViewDto
    {
        public Guid? Guid { get; set; }
        public string CartId { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }
        public ProductViewDto? Product { get; set; }
    }

    public class OrderDetailViewDto
    {
        public Guid? Guid { get; set; }
        public int Count { get; set; }
        public decimal UnitPrice { get; set; }
        public OrderViewDto? Order { get; set; }
        public ProductViewDto? Product { get; set; }
    }
}
