namespace MinimalAPIERP.Dtos
{
    public class RaincheckDto
    {
        public string? Name { get; set; }
        public int Count { get; set; }
        public double SalePrice { get; set; }
    }

    public class RaincheckViewDto
    {
        public Guid Guid { get; set; }
        public string? Name { get; set; }
        public int Count { get; set; }
        public double SalePrice { get; set; }
        public StoreViewDto? Store { get; set; }
        public ProductViewDto? Product { get; set; }
    }

    public class StoreDto
    {
        public string? Name { get; set; }
    }

    public class StoreViewDto
    {
        public Guid? Guid { get; set; }
        public string? Name { get; set; }
    }

    public class ProductDto
    {
        public string? Name { get; set; }
        public CategoryDto? Category { get; set; }
    }

    public class ProductViewDto
    {
        public string? Name { get; set; }
        public CategoryDto? Category { get; set; }
    }


    public class CategoryDto
    {
        public string? Name { get; set; }
    }
}
