using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace CSharpExam.Models
{
    public class Activity 
    {
        [Key]
        public int ActivityId {get;set;}

        [Required(ErrorMessage="Title cannot be blank.")]
        [MinLength(2)]
        public string Title { get; set; }
 
        [Required(ErrorMessage="Description cannot be blank.")]
        [MinLength(10)]
        public string Description { get; set; }

        [Required(ErrorMessage="Date and Time is required")]
        [DataType(DataType.DateTime)]
        [FutureDate(ErrorMessage="Date and Time should be in the future.")]
        public DateTime DateTime {get;set;}

        [Required(ErrorMessage="Duration cannot be blank")]
        public int Duration {get;set;}

        [Required]
        public String Unit {get;set;}
        public int UserId {get;set;}
        public User Creator {get;set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}

        public List<Activitycenter> participants {get;set;} 
        public Activity(){
            participants = new List<Activitycenter>();
        }
    }
}