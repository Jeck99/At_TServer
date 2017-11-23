using AT_T_31_10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AT_T_31_10.Utils
{
    public class AdjustmentUtils
    {
        private AT_T_31_10Context db = new AT_T_31_10Context();


        public float GetBestMatchPrecentage(int ApplicantId, int RecruiterId)
        {
            float Precentage = 0f;

            
                var Skillset = db.ApplicantSkillsets.Where(AppSkl => AppSkl.ApplicantId == ApplicantId);
                var JobsRelated = db.JobRecruiters.Where(AppSkl => AppSkl.RecruiterId == RecruiterId);
                if (Skillset == null || JobsRelated == null)
                    return 0;

                foreach (JobRecruiter jobr in JobsRelated)
                {
                    float precente = (float) GetMatchPrecentage(jobr.JobId, Skillset);
                    if (Precentage < precente)
                        Precentage = (float) precente ;
                }

            return (float)Precentage;

        }

        public float GetMatchPrecentage(int JobId, IEnumerable<ApplicantSkillset> ApplicantSkillset)
        {
            float MatchPrecentage = 0f;
            var JobsSkilset = db.JobSkillsets.Where(Jk => Jk.JobId == JobId).ToList();
            float SkillsAmount = JobsSkilset.Count();
            foreach (ApplicantSkillset applicantskil in ApplicantSkillset)
            {
                if (JobsSkilset.Exists(Js => Js.SkillId == applicantskil.SkillId))
                    MatchPrecentage++;
            }




            return  ((float) MatchPrecentage / (float)SkillsAmount) ;
        }






    }
}