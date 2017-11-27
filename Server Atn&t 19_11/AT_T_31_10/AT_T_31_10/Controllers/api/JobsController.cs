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
using AT_T_31_10.Utils;

namespace AT_T_31_10.Controllers.api
{
    public class JobsController : ApiController
    {
        private AT_T_31_10Context db = new AT_T_31_10Context();
         private Security Auth = new Security();

        // GET: api/Jobs
        public IHttpActionResult  GetJobs()
        {
            try
            {
                var head = this.Request.Headers.GetValues("UserKey").FirstOrDefault();
                if (!Auth.AuthSecure(head))
                    return Content(HttpStatusCode.BadRequest, "UnAuthorized");
                var Result = (
from jb in db.Jobs
select new
{
    Id = jb.Id,
    Title = jb.Title,
    Position = jb.Position,
    Description = jb.Description,
    Published = jb.Published,
    Experience = jb.Experience
    ,
    Skills =
    from skl in db.Skillsets
    .Where(SkSet =>
    (db.JobSkillsets
    .Where(JobSK => JobSK.JobId == jb.Id)
    .Select(jobskill => jobskill.SkillId))
    .Contains((int)SkSet.Id)
    )
    select skl
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


                return Ok(Result);
            }
            catch (Exception error)
            {
                return Content(HttpStatusCode.BadRequest, "UnAuthorized - session couldnt be found ");
            }

        }

        // GET: api/Jobs/5
        [ResponseType(typeof(Job))]
        public async Task<IHttpActionResult> GetJob(int id)
        {
            try
            {
                var head = this.Request.Headers.GetValues("UserKey").FirstOrDefault();
                if (!Auth.AuthSecure(head))
                    return Content(HttpStatusCode.BadRequest, "UnAuthorized");
                Job job = await db.Jobs.FindAsync(id);
                if (job == null)
                {
                    return NotFound();
                }

                return Ok(job);
            }
            catch (Exception error)
            {
                return Content(HttpStatusCode.BadRequest, "UnAuthorized - session couldnt be found ");
            }



        }

        // PUT: api/Jobs/5
        [HttpPut]
        public async Task<IHttpActionResult> PutJob(int id, Job job)
        {
            try
            {
                var head = this.Request.Headers.GetValues("UserKey").FirstOrDefault();
                if (!Auth.AuthSecure(head))
                    return Content(HttpStatusCode.BadRequest, "UnAuthorized");

                int CurrentUserId = Auth.GetId(head);

                var JobRelation =db.JobRecruiters.Where(Jb => Jb.JobId == job.Id && Jb.RecruiterId == CurrentUserId);

                if(JobRelation==null)
                {
                    return Content(HttpStatusCode.BadRequest, "UnAuthorized - Only The Job Manager Recruiter");
                }
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
            catch (Exception error)
            {
                return Content(HttpStatusCode.BadRequest, "UnAuthorized - session couldnt be found ");
            }


        }

        // POST: api/Jobs
        [HttpPost]
        public IHttpActionResult PostJob(Job job)
        {

            try
            {
                var head = this.Request.Headers.GetValues("UserKey").FirstOrDefault();
                if (!Auth.AuthSecure(head))
                    return Content(HttpStatusCode.BadRequest, "UnAuthorized");

                if (!Auth.IsAdmin(head))
                    return Content(HttpStatusCode.BadRequest, "UnAuthorized");


                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Jobs.Add(job);
                CreatedAtRoute("DefaultApi", new { id = job.Id }, job);

                db.SaveChanges();


                return Ok(job.Id);
            }
            catch (Exception error)
            {
                return Content(HttpStatusCode.BadRequest, "UnAuthorized - session couldnt be found ");
            }
        }

        [ResponseType(typeof(Job))]
        public async Task<IHttpActionResult> DeleteJob(int id)
        {

            try
            {
                var head = this.Request.Headers.GetValues("UserKey").FirstOrDefault();
                if (!Auth.AuthSecure(head))
                    return Content(HttpStatusCode.BadRequest, "UnAuthorized");

                if (!Auth.IsAdmin(head))
                    return Content(HttpStatusCode.BadRequest, "UnAuthorized");


                Job job = await db.Jobs.FindAsync(id);
                if (job == null)
                {
                    return NotFound();
                }

                db.Jobs.Remove(job);
                await db.SaveChangesAsync();
                return Ok(job);

            }
            catch (Exception error)
            {
                return Content(HttpStatusCode.BadRequest, "UnAuthorized - session couldnt be found ");
            }


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