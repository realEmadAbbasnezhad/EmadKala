using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

// ReSharper disable PropertyCanBeMadeInitOnly.Global
#pragma warning disable CS8618
namespace EmadKala.Database;

public class FileModel
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }

    [MaxLength(20)] public string Source { get; set; }
    [MaxLength(20)] public string Type { get; set; }
}

public class EmadKalaFileDbContext(DbContextOptions<EmadKalaFileDbContext> opt) : DbContext(opt)
{
    public DbSet<FileModel> Files { get; set; }
    
    public static async Task WriteUserFileAsync(int id, byte[]? data)
    {
        var filePath = $"/fileSystem/{id}";

        // delete
        if (data == null)
        {
            if (File.Exists(filePath)) File.Delete(filePath);
            return;
        }

        // write
        if (!File.Exists(filePath)) File.Create(filePath).Close();
        await File.WriteAllBytesAsync(filePath, data);
    }

    public static async Task<byte[]?> ReadUserFileAsync(int id)
    {
        var filePath = $"/fileSystem/{id}";

        // read
        if (!File.Exists(filePath)) return null;
        return await File.ReadAllBytesAsync(filePath);
    }
}