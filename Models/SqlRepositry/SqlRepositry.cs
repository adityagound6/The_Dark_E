using FoodRest.ViewModel.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodRest.Models.SqlRepositry
{
    public class SqlRepositry :IRepositry
    {
        private AppDbContext context;
        public SqlRepositry(AppDbContext context)
        {
            this.context = context;
        }

        public Users AddUsers(Users user)
        {
            //Users user1 = new Users();
            context.Add(user);
            context.SaveChanges();
            return user;
            //throw new NotImplementedException();
        }

        public Users GetUserById(LogInViewModel model)
        {
            var user = context.Users.Where(x => x.Email == model.Email).FirstOrDefault();
            return user;
            throw new NotImplementedException();
        }

        public bool IsUser(LogInViewModel model)
        {
            bool isUser = context.Users.Any(x => x.Email == model.Email && x.Password == model.Password);
            return isUser;
            //throw new NotImplementedException();
        }
    }
}
