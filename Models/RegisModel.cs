using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System;

namespace CSharpExam.Models
{
    public class RegisUser : BaseEntity
    {
        [Key]
        public int UserID {get;set;}

        [Required]
        [MinLength(3)]
        public string Firstname { get; set; }
        
        [Required]
        [MinLength(3)]
        public string Lastname { get; set; }
 
        [Required]
        [EmailAddress]
        [MinLength(3)]
        public string Email { get; set; }
 
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password {get; set;}

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword {get;set;}
    }
}