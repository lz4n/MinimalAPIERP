namespace BlazorApp.Dtos
{
    public record OrderDto
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

    public record CategoryDto
    {
        public Guid? Guid { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }

    public record RaincheckDto
    {
        public Guid? Guid { get; set; }
        public string? Name { get; set; }
        public int Count { get; set; }
        public double SalePrice { get; set; }
        public StoreDto? Store { get; set; }
        public ProductDto? Product { get; set; }
    }

    public record StoreDto
    {
        public Guid? Guid { get; set; }
        public string? Name { get; set; }
    }

    public record ProductDto
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
        public CategoryDto? Category { get; set; }
    }

    public record CartItemDto
    {
        public Guid? Guid { get; set; }
        public string CartId { get; set; }
        public int Count { get; set; }
        public DateTime DateCreated { get; set; }
        public ProductDto? Product { get; set; }
    }

    public record OrderDetailDto
    {
        public Guid? Guid { get; set; }
        public int Count { get; set; }
        public decimal UnitPrice { get; set; }
        public OrderDto? Order { get; set; }
        public ProductDto? Product { get; set; }
    }
}
