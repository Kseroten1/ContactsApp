using System.ComponentModel.DataAnnotations;

namespace ContactsApp.Models
{
    public class SubCategory
    {
        public int SubCategoryId { get; set; }

        [Required]
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }
    }
}
