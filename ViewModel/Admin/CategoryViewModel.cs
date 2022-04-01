using FoodRest.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodRest.ViewModel.Admin
{
    public class CategoryViewModel : Categories
    {
        public IFormFile Photo { get; set; }
    }
}
