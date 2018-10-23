using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System;

namespace CSharpExam.Models
{
    public class BaseEntity {}
    public class User : BaseEntity
    {
        [Key]
        public int UserId {get;set;}
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string Email { get; set; }
    
        public string Password {get; set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}

        public List<Activitycenter> activities { get; set; }
        public User(){
            activities = new List<Activitycenter>();
        }
    }
}
    