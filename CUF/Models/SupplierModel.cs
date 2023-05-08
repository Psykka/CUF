using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CUF.Models;

[Index(nameof(CNPJ), IsUnique = true)]
public class SupplierModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    // Razão Social
    [Required]
    public string? Company { get; set; }

    // Nome Fantasia
    public string? Trade { get; set; }

    [Required]
    public string? CNPJ { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? CRF { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ICollection<CategoryModel>? Category { get; set; }

    public ICollection<ProductModel>? Product { get; set; }
}