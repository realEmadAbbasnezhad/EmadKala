using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

// ReSharper disable PropertyCanBeMadeInitOnly.Global
#pragma warning disable CS8618
namespace EmadKala.Database;

public class CategoryModel
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public short Id { get; set; }
    
    [MaxLength(100)] public string Name { get; set; }
    public short? Parent { get; set; }
}

public class ProductModel
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    
    public int Category { get; set; }
    public int SubCategory { get; set; }

    [MaxLength(100)] public string PriceUnit { get; set; }
    public ulong Price { get; set; }
    
    [MaxLength(100)] public string Title { get; set; }
    public List<int> Medias { get; set; }
    [MaxLength(500)] public string Description { get; set; }
}

public class EmadKalaStoreDbContext(DbContextOptions<EmadKalaStoreDbContext> opt) : DbContext(opt)
{
    public DbSet<CategoryModel> SubCategories { get; set; }
    
    public static readonly List<CategoryModel> Categories =
    [
        new CategoryModel { Id = 1, Name = "کلاینت" },
        new CategoryModel { Id = 2, Name = "سرور" }
    ];
}
