using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodRest.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public int Cvv { get; set; }
        public string Address { get; set; }
        public string PaymentMode { get; set; }
    }
}
