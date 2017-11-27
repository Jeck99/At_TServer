using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AT_T_31_10.Models
{
    public class Applicant
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public float Experience { get; set; }
        public string Position { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public string LockedBy { get; set; }
        public bool Active { get; set; }
        public string Url { get; set; }

    }
}