using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AT_T_31_10.Models
{
    public class Review
    {
        public int Id { get; set;}
        public int ApplicantId { get; set;}
        public int ManagerId { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Content { get; set;}
    }
}