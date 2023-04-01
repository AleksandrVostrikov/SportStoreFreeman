using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace SportStoreFreeman.Models
{
    public class Order
    {
        [BindNever]
        public int OrderID { get; set; }
        [BindNever]
        public ICollection<CartLine>? Lines { get; set; }
        
        [Required(ErrorMessage ="Введите ФИО")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Введите улицу, номер дома, квартиру")]
        public string Line1 { get; set; }
        [Required(ErrorMessage ="Введите населенный пункт")]
        public string City { get; set; }
        [Required(ErrorMessage ="Введите область, район")]
        public string State { get; set; }
        [Required(ErrorMessage = "Введите индекс")]
        public string Zip { get; set; }
        public bool GiftWrap { get; set; }
    }
}
