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
    public class JobsController : ApiController
    {
        private AT_T_31_10Context db = new AT_T_31_10Context();

        // GET: api/Jobs
        public IEnumerable<object> GetJobs()
        {
           var Result= (
           from jb in db.Jobs
           select new
           {
               Id = jb.Id,
               Title = jb.Title,
               Position = jb.Position,
               Description = jb.Description,
               Published = jb.Published,
               Active = jb.Active,
               Experience = jb.Experience
               ,
               Skills =
               from skl in db.Skillsets
               .Where(SkSet => 
               (db.JobSkillsets
               .Where(JobSK => JobSK.JobId ==jb.Id)
               .Select(jobskill=>jobskill.SkillId))
               .Contains((int)SkSet.Id)
               ) select skl
               ,
               Recruiters =
               from manag in db.Managers
                            .Where(man =>
                            (db.JobRecruiters
                            .Where(R => R.JobId == jb.Id)
                            .Select(re => re.RecruiterId))
                            .Contains(man.Id))
                            select new
                            {
                                Id = manag.Id,
                                Email = manag.Email,
                                UserName = manag.UserName
                            }
           }).ToList();


         
    

            


            //var quer = from re in db.Managers
            //           select new
            //           {
            //               Id = db.JobRecruiters.FirstOrDefault(R => R.JobId == jb.Id).Select(re => re.RecruiterId),
            //           };

            return Result;
        }

        // GET: api/Jobs/5
        [ResponseType(typeof(Job))]
        public async Task<IHttpActionResult> GetJob(int id)
        {
            Job job = await db.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            return Ok(job);
        }

        // PUT: api/Jobs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutJob(int id, Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != job.Id)
            {
                return BadRequest();
            }

            db.Entry(job).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
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

        // POST: api/Jobs
        [HttpPost]
        public IHttpActionResult PostJob(Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Jobs.Add(job);
            CreatedAtRoute("DefaultApi", new { id = job.Id }, job);

            db.SaveChanges();


            return Ok(job.Id);
        }

        // DELETE: api/Jobs/5
        [ResponseType(typeof(Job))]
        public async Task<IHttpActionResult> DeleteJob(int id)
        {
            Job job = await db.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            db.Jobs.Remove(job);
            await db.SaveChangesAsync();

            return Ok(job);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobExists(int id)
        {
            return db.Jobs.Count(e => e.Id == id) > 0;
        }
    }
}