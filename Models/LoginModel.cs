using System.ComponentModel.DataAnnotations;
using System;

namespace CSharpExam.Models
{
    public class LoginUser : BaseEntity
    {
        [Required(ErrorMessage="Email or Password is incorrect.")]
        [EmailAddress]
        [Display(Name="Email")]
        public string LogEmail {get;set;}

        [Required(ErrorMessage="Email or Password is incorrect.")]
        [Display(Name="Password")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}
    }
    
}