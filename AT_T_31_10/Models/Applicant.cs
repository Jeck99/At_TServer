using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AT_T_31_10.Models
{
    public class Applicant
    {
        public int Id { get; set; }
        public int Experience { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public string Skillset { get; set;}
        public DateTime Date { get; set; }
        public string LockedBy { get; set; }
        public bool Active { get; set; }

    }
}