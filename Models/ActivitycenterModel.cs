using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;
namespace CSharpExam.Models
{
    public class Activitycenter
    {
        [Key]
        public int CenterId {get;set;}
        public int ActivityId {get;set;}
        public Activity Activity {get;set;}
        public int UserId {get;set;}
        public User User {get;set;}
    }
}