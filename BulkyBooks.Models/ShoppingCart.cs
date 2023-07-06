using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class ShoppingCart
    {
        public Product Product { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "enter number between 1 to 1000")]
        public int Count { get; set; }
    }
}
