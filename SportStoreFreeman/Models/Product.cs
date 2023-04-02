using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportStoreFreeman.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }

        [Required(ErrorMessage ="Введите название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите описание")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage ="Цена должна быть больше нуля")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Укажите категорию")]
        public string Category { get; set; }
    }
}
