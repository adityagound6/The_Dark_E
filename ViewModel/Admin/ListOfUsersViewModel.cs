using FoodRest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodRest.ViewModel.Admin
{
    public class ListOfUsersViewModel
    {
        public List<Users> ListOfUser { get; set; }
        public Users user { get; set; }
    }
}
