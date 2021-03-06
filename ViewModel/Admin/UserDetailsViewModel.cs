using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodRest.ViewModel.Admin
{
    public class UserDetailsViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public string Role { get; set; }
    }
}
