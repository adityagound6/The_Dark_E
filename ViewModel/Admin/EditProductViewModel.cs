using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodRest.Models;

namespace FoodRest.ViewModel.Admin
{
    public class EditProductViewModel : Product
    {
        public string NewPhoto { get; set; }
    }
}
