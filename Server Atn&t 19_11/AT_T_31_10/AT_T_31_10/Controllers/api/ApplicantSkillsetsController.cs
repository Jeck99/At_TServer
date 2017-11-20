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
    public class ApplicantSkillsetsController : ApiController
    {
        private AT_T_31_10Context db = new AT_T_31_10Context();

        // GET: api/ApplicantSkillsets
        public IQueryable<ApplicantSkillset> GetApplicantSkillsets()
        {
            return db.ApplicantSkillsets;
        }

        // GET: api/ApplicantSkillsets/5
        [ResponseType(typeof(ApplicantSkillset))]
        public async Task<IHttpActionResult> GetApplicantSkillset(long id)
        {
            ApplicantSkillset applicantSkillset = await db.ApplicantSkillsets.FindAsync(id);
            if (applicantSkillset == null)
            {
                return NotFound();
            }

            return Ok(applicantSkillset);
        }

        // PUT: api/ApplicantSkillsets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutApplicantSkillset(long id, ApplicantSkillset [] applicantSkillset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Applicantskills = db.ApplicantSkillsets.Where(JobSkill => JobSkill.ApplicantId == id);

            if (Applicantskills == null)
            {
                return NotFound();
            }

            db.ApplicantSkillsets.RemoveRange(Applicantskills);

            for (int i = 0; i < applicantSkillset.Length; i++)
            {
                db.ApplicantSkillsets.Add(applicantSkillset[i]);
                await db.SaveChangesAsync();
                CreatedAtRoute("DefaultApi", new { id = applicantSkillset[i].Id }, applicantSkillset[i]);
            }


            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicantSkillsetExists(id))
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

        // POST: api/ApplicantSkillsets
        [ResponseType(typeof(ApplicantSkillset))]
        public async Task<IHttpActionResult> PostApplicantSkillset(ApplicantSkillset [] applicantSkillset )
        {
            for (int i = 0; i < applicantSkillset.Length; i++)
            {
                db.ApplicantSkillsets.Add(applicantSkillset[i]);
                await db.SaveChangesAsync();
                CreatedAtRoute("DefaultApi", new { id = applicantSkillset[i].Id }, applicantSkillset[i]);

            }
            return Ok(); ;
        }

        // DELETE: api/ApplicantSkillsets/5
        [ResponseType(typeof(ApplicantSkillset))]
        public async Task<IHttpActionResult> DeleteApplicantSkillset(long id)
        {
            ApplicantSkillset applicantSkillset = await db.ApplicantSkillsets.FindAsync(id);
            if (applicantSkillset == null)
            {
                return NotFound();
            }

            db.ApplicantSkillsets.Remove(applicantSkillset);
            await db.SaveChangesAsync();

            return Ok(applicantSkillset);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicantSkillsetExists(long id)
        {
            return db.ApplicantSkillsets.Count(e => e.Id == id) > 0;
        }
    }
}