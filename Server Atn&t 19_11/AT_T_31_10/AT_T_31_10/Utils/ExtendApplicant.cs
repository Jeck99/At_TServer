using AT_T_31_10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AT_T_31_10.Utils
{
    public class ExtendApplicant 
    {

        public long Id { get; set; }
        public string Title { get; set; }
        public int Experience { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string LockedBy { get; set; }
        public bool Active { get; set; }
        public string Url { get; set; }

        public IQueryable<Skillset> Skills { get; set; }

        public IQueryable<dynamic> Recruiters { get; set; }

        public float  MatchPrecentage{ get; set; }
    }
}