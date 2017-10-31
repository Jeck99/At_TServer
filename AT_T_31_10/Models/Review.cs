using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AT_T_31_10.Models
{
    public class Review
    {
        public int Id { get; set;}
        public int ApplicantId { get; set;}
        public int ManagerId { get; set; }
        public string Status { get; set; }
        public string Content { get; set;}
    }
}