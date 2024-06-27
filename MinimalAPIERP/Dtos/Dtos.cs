using ERP;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MinimalAPIERP.Dtos
{
    public class OrderDto
    {
        public DateTime OrderDate { get; set; }
        public string Username { get; set; } = null!;

        [StringLength(160)]
        public string Name { get; set; } = null!;

        [StringLength(70)]
        public string Address { get; set; } = null!;

        [StringLength(40)]
        public string City { get; set; } = null!;

        [StringLength(40)]
        public string State { get; set; } = null!;

        [StringLength(10)]
        public string PostalCode { get; set; } = null!;

        [StringLength(40)]
        public string Country { get; set; } = null!;

        [StringLength(24)]
        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; }
    }

    public class OrderViewDto
    {
        public Guid? Guid { get; set; }
        public DateTime OrderDate { get; set; }
        public string Username { get; set; } = null!;

        [StringLength(160)]
        public string Name { get; set; } = null!;

        [StringLength(70)]
        public string Address { get; set; } = null!;

        [StringLength(40)]
        public string City { get; set; } = null!;

        [StringLength(40)]
        public string State { get; set; } = null!;

        [StringLength(10)]
        public string PostalCode { get; set; } = null!;

        [StringLength(40)]
        public string Country { get; set; } = null!;

        [StringLength(24)]
        public string Phone { get; set; } = null!;

        public string Email { get; set; } = null!;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Total { get; set; }
    }

    public class RaincheckDto
    {
        public string? Name { get; set; }
        public int Count { get; set; }
        public double SalePrice { get; set; }
        public Guid? StoreGuid { get; set; }
        public Guid? ProductGuid { get; set; }
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
