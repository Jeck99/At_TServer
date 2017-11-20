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
    public class ApplicantRecruitersController : ApiController
    {
        private AT_T_31_10Context db = new AT_T_31_10Context();

        // GET: api/ApplicantRecruiters
        public IQueryable<ApplicantRecruiters> GetApplicantRecruiters()
        {
            return db.ApplicantRecruiters;
        }

        // GET: api/ApplicantRecruiters/5
        [ResponseType(typeof(ApplicantRecruiters))]
        public async Task<IHttpActionResult> GetApplicantRecruiters(long id)
        {
            ApplicantRecruiters applicantRecruiters = await db.ApplicantRecruiters.FindAsync(id);
            if (applicantRecruiters == null)
            {
                return NotFound();
            }

            return Ok(applicantRecruiters);
        }

        // PUT: api/ApplicantRecruiters/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutApplicantRecruiters(long id, ApplicantRecruiters [] applicantRecruiters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ApplicantRecruiters = db.ApplicantRecruiters.Where(Applicantrecruiter=> Applicantrecruiter.ApplicantId == id);

            if (ApplicantRecruiters == null)
            {
                return NotFound();
            }

            db.ApplicantRecruiters.RemoveRange(ApplicantRecruiters);

            for (int i = 0; i < applicantRecruiters.Length; i++)
            {
                db.ApplicantRecruiters.Add(applicantRecruiters[i]);
                await db.SaveChangesAsync();
                CreatedAtRoute("DefaultApi", new { id = applicantRecruiters[i].Id }, applicantRecruiters[i]);
            }

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicantRecruitersExists(id))
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

        // POST: api/ApplicantRecruiters
        [ResponseType(typeof(ApplicantRecruiters))]
        public async Task<IHttpActionResult> PostApplicantRecruiters(ApplicantRecruiters [] applicantRecruiters)
        {
            for (int i = 0; i < applicantRecruiters.Length; i++)
            {
                db.ApplicantRecruiters.Add(applicantRecruiters[i]);
                await db.SaveChangesAsync();
                CreatedAtRoute("DefaultApi", new { id = applicantRecruiters[i].Id }, applicantRecruiters[i]);
            }
            return Ok();
        }

        // DELETE: api/ApplicantRecruiters/5
        [ResponseType(typeof(ApplicantRecruiters))]
        public async Task<IHttpActionResult> DeleteApplicantRecruiters(long id)
        {
            ApplicantRecruiters applicantRecruiters = await db.ApplicantRecruiters.FindAsync(id);
            if (applicantRecruiters == null)
            {
                return NotFound();
            }

            db.ApplicantRecruiters.Remove(applicantRecruiters);
            await db.SaveChangesAsync();

            return Ok(applicantRecruiters);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicantRecruitersExists(long id)
        {
            return db.ApplicantRecruiters.Count(e => e.Id == id) > 0;
        }
    }
}