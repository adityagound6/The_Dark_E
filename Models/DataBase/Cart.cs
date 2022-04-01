using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodRest.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public int Qantity { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
    }
}
