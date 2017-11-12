using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AT_T_31_10.Models;

namespace AT_T_31_10.Controllers.api
{
    public class ApplicantsController : ApiController
    {
        private AT_T_31_10Context db = new AT_T_31_10Context();

        // GET: api/Applicants
        public IEnumerable<object> GetApplicants()
        {
            var Result = (
from Applican in db.Applicants
select new
{
    Id = Applican.Id,
    Title = Applican.Title,
    LockedBy = Applican.LockedBy,
    Date = Applican.Date,
    Active = Applican.Active,
    Experience = Applican.Experience,
    Name = Applican.Name,
    Status = Applican.Status,
    Email = Applican.Email
    ,
    Skills =
    from skl in db.Skillsets
    .Where(SkSet =>
    (db.ApplicantSkillsets
    .Where(JobSK => JobSK.ApplicantId == Applican.Id)
    .Select(jobskill => jobskill.SkillId))
    .Contains((int)SkSet.Id)
    )
    select skl
    ,
    Recruiters =
    from manag in db.Managers
                 .Where(man =>
                 (db.ApplicantRecruiters
                 .Where(R => R.ApplicantId == Applican.Id)
                 .Select(re => re.RecruiterId))
                 .Contains(man.Id))
    select new
    {
        Id = manag.Id,
        Email = manag.Email,
        UserName = manag.UserName
    }
}).ToList();







            return Result;
        }

        // GET: api/Applicants/5
        [ResponseType(typeof(Applicant))]
        public async Task<IHttpActionResult> GetApplicant(int id)
        {
            Applicant applicant = await db.Applicants.FindAsync(id);
            if (applicant == null)
            {
                return NotFound();
            }

            return Ok(applicant);
        }

        // PUT: api/Applicants/5
        [ResponseType(typeof(void))]
        [Authorize]
        public async Task<IHttpActionResult> PutApplicant(int id, Applicant applicant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicant.Id)
            {
                return BadRequest();
            }

            db.Entry(applicant).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Applicants
        [HttpPost]
        public  IHttpActionResult PostApplicant(Applicant applicant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            applicant.Date = DateTime.Now;
            applicant.LockedBy = "";

            db.Applicants.Add(applicant);
            CreatedAtRoute("DefaultApi", new { id = applicant.Id }, applicant);
            db.SaveChanges();

            return Ok(applicant.Id);

        }

        // DELETE: api/Applicants/5
        [ResponseType(typeof(Applicant))]
        public async Task<IHttpActionResult> DeleteApplicant(int id)
        {
            Applicant applicant = await db.Applicants.FindAsync(id);
            if (applicant == null)
            {
                return NotFound();
            }

            db.Applicants.Remove(applicant);
            await db.SaveChangesAsync();

            return Ok(applicant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicantExists(int id)
        {
            return db.Applicants.Count(e => e.Id == id) > 0;
        }
    }
}