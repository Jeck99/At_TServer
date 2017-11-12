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
    public class JobSkillsetsController : ApiController
    {
        private AT_T_31_10Context db = new AT_T_31_10Context();

        // GET: api/JobSkillsets /// no Controller of Skillset
        public IQueryable<Skillset> GetJobSkillsets()
        {
            return db.Skillsets;
        }

        // GET: api/JobSkillsets/5
        [ResponseType(typeof(JobSkillset))]
        public async Task<IHttpActionResult> GetJobSkillset(long id)
        {
            JobSkillset jobSkillset = await db.JobSkillsets.FindAsync(id);
            if (jobSkillset == null)
            {
                return NotFound();
            }

            return Ok(jobSkillset);
        }

        // PUT: api/JobSkillsets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutJobSkillset(long id, JobSkillset [] jobSkillset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var JobSkills = db.JobSkillsets.Where(JobSkill => JobSkill.JobId == id);

            if (JobSkills==null)
            {
                return NotFound();
            }

            db.JobSkillsets.RemoveRange(JobSkills);

            for (int i = 0; i < jobSkillset.Length; i++)
            {
                db.JobSkillsets.Add(jobSkillset[i]);
                await db.SaveChangesAsync();
                CreatedAtRoute("DefaultApi", new { id = jobSkillset[i].Id }, jobSkillset[i]);
            }


            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobSkillsetExists(id))
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

        // POST: api/JobSkillsets
        [ResponseType(typeof(JobSkillset))]
        public async Task<IHttpActionResult> PostJobSkillset(JobSkillset [] jobSkillset)
        {
            for (int i = 0; i < jobSkillset.Length; i++)
            {
                db.JobSkillsets.Add(jobSkillset[i]);
                await db.SaveChangesAsync();
                CreatedAtRoute("DefaultApi", new { id = jobSkillset[i].Id }, jobSkillset[i]);

            }
            return Ok();
        }

        // DELETE: api/JobSkillsets/5
        [ResponseType(typeof(JobSkillset))]
        public async Task<IHttpActionResult> DeleteJobSkillset(long id)
        {
            JobSkillset jobSkillset = await db.JobSkillsets.FindAsync(id);
            if (jobSkillset == null)
            {
                return NotFound();
            }

            db.JobSkillsets.Remove(jobSkillset);
            await db.SaveChangesAsync();

            return Ok(jobSkillset);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JobSkillsetExists(long id)
        {
            return db.JobSkillsets.Count(e => e.Id == id) > 0;
        }
    }
}