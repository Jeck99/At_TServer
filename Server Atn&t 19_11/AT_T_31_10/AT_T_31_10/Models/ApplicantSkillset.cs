using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AT_T_31_10.Models
{
    public class ApplicantSkillset
    {
        public long Id { get; set; }
        public int ApplicantId { get; set;}
        public int SkillId { get; set; }
    }
}