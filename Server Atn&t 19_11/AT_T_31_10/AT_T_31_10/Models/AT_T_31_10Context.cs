using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AT_T_31_10.Models
{
    public class AT_T_31_10Context : DbContext
    {
    
        public AT_T_31_10Context() : base("name=AT_T_31_10Context")
        {
        }

        public System.Data.Entity.DbSet<AT_T_31_10.Models.Applicant> Applicants { get; set; }

        public System.Data.Entity.DbSet<AT_T_31_10.Models.Job> Jobs { get; set; }

        public System.Data.Entity.DbSet<AT_T_31_10.Models.Manager> Managers { get; set; }

        public System.Data.Entity.DbSet<AT_T_31_10.Models.Review> Reviews { get; set; }

        public System.Data.Entity.DbSet<AT_T_31_10.Models.ManagerLogins> ManagerLogins { get; set; }

        public System.Data.Entity.DbSet<AT_T_31_10.Models.Skillset> Skillsets { get; set; }

        public System.Data.Entity.DbSet<AT_T_31_10.Models.ApplicantSkillset> ApplicantSkillsets { get; set; }

        public System.Data.Entity.DbSet<AT_T_31_10.Models.JobSkillset> JobSkillsets { get; set; }

        public System.Data.Entity.DbSet<AT_T_31_10.Models.JobRecruiter> JobRecruiters { get; set; }

        public System.Data.Entity.DbSet<AT_T_31_10.Models.ApplicantRecruiters> ApplicantRecruiters { get; set; }
    }
}
