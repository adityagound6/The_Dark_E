using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodRest.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int OrderNumber { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public string Status { get; set; }
        [ForeignKey("Payment")]
        public int PaymentId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
