using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodRest.ViewModel.Accounts
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage ="Email should contain @gmail.com")]
        public string Email { get; set; }
        public string UserName { get; set; }
        [Required]
        [MaxLength(20,ErrorMessage ="Name should be in 6-20 character")]
        [MinLength(6,ErrorMessage ="Name shold be in 6-20 character")]
        public string Name { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [MaxLength(15,ErrorMessage ="Password should in 8-15 char")]
        [MinLength(8,ErrorMessage = "Password should in 8-15 char")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password and Confirm password must be match")]
        public string ConfirmPassword { get; set; }
        [StringLength(100,ErrorMessage ="Address can be in 100 word")]
        public string Address { get; set; }
        
        [MaxLength(6,ErrorMessage ="Post code must be 6 digit")]
        [MinLength(6,ErrorMessage ="Post Code must be 6 digit")]
        public string PostCode { get; set; }
        public IFormFile Photo { get; set; }
        public string Role { get; set; }
    }
}
