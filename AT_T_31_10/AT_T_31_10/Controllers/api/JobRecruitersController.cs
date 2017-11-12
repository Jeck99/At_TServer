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
    public class JobRecruitersController : ApiController
    {
        private AT_T_31_10Context db = new AT_T_31_10Context();

        // GET: api/JobRecruiters
        public IQueryable<JobRecruiter> GetJobRecruiters()
        {
            return db.JobRecruiters;
        }

        // GET: api/JobRecruiters/5
        [ResponseType(typeof(JobRecruiter))]
        public async Task<IHttpActionResult> GetJobRecruiter(long id)
        {
            JobRecruiter jobRecruiter = await db.JobRecruiters.FindAsync(id);
            if (jobRecruiter == null)
            {
                return NotFound();
            }

            return Ok(jobRecruiter);
        }

        // PUT: api/JobRecruiters/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutJobRecruiter(long id, JobRecruiter jobRecruiter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobRecruiter.Id)
            {
                return BadRequest();
            }

            db.Entry(jobRecruiter).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobRecruiterExists(id))
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

        // POST: api/JobRecruiters
        [ResponseType(typeof(JobRecruiter))]
        public async Task<IHttpActionResult> PostJobRecruiter(JobRecruiter [] jobRecruiter)
        {
            for (int i = 0; i < jobRecruiter.Length; i++)
            {
                db.JobRecruiters.Add(jobRecruiter[i]);
                await db.SaveChangesAsync();
                CreatedAtRoute("DefaultApi", new { id = jobRecruiter[i].Id }, jobRecruiter[i]);
            }
            return Ok();
        }

        // DELETE: api/JobRecruiters/5
        [ResponseType(typeof(JobRecruiter))]
        public async Task<IHttpActionResult> DeleteJobRecruiter(long id)
        {
            JobRecruiter jobRecruiter = await db.JobRecruiters.FindAsync(id);
            if (jobRecruiter == null)
            {
                return NotFound();
            }

            db.JobRecruiters.Remove(jobRecruiter);
            await db.SaveChangesAsync();

            return Ok(jobRecruiter);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobRecruiterExists(long id)
        {
            return db.JobRecruiters.Count(e => e.Id == id) > 0;
        }
    }
}