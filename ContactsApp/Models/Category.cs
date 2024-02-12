using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Models;

public class Category
{
    public int CategoryId { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    public bool AllowCustomSubCategory { get; set; } = true;
}
