using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AT_T_31_10.Models
{
    public class JobSkillset
    {
        public long Id { get; set; }
        public int JobId { get; set; }
        public int SkillId { get; set;}
    }
}