using FoodRest.ViewModel.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodRest.Models.SqlRepositry
{
    public interface IRepositry
    {
        public Users AddUsers(Users user);
        public bool IsUser(LogInViewModel model);
        public Users GetUserById(LogInViewModel model);
    }
}
