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
    public class ReviewsController : ApiController
    {
        private AT_T_31_10Context db = new AT_T_31_10Context();
        private Security Auth = new Security();

        [HttpGet]
        public IHttpActionResult GetReviews()
        {
            try
            {
                var head = this.Request.Headers.GetValues("UserKey").FirstOrDefault();
                if (!Auth.AuthSecure(head))
                    return Content(HttpStatusCode.BadRequest, "UnAuthorized");

               int UserId = Auth.GetId(head);

              var UserReviews = db.Reviews.Where(rev => rev.ManagerId == UserId && rev.Status=="").Select(R => R.ApplicantId);

                var UserLockedByRecruiter = db.Applicants.Where(App => UserReviews.Contains(App.Id) && App.Active);

                return Ok(UserLockedByRecruiter);

            }
            catch (Exception error)
            {
                return Content(HttpStatusCode.BadRequest, "UnAuthorized - session couldnt be found ");
            }
        }

        //[ResponseType(typeof(Review))]
        //public async Task<IHttpActionResult> GetReview(int id)
        //{
        //    Review review = await db.Reviews.FindAsync(id);
        //    if (review == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(review);
        //}

        [HttpPut]
        public IHttpActionResult PutReview(Review review)
        {
            var ReviewToSeald =db.Reviews.FirstOrDefault(rev => 
            rev.ManagerId == review.ManagerId
            && rev.ApplicantId == review.ApplicantId);
     
                if(ReviewToSeald == null)
                return NotFound();


            ReviewToSeald.Content = review.Content;
            ReviewToSeald.Status = review.Status;

            if (review.Status == "Fail" || review.Status == "Pass")
            {
                var applicantToArchived = db.Applicants.Find(review.ApplicantId);
                applicantToArchived.Active = false;
            }
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        public IHttpActionResult PostReview(Review review)
        {
            try
            {
                var head = this.Request.Headers.GetValues("UserKey").FirstOrDefault();
                if (!Auth.AuthSecure(head))
                    return Content(HttpStatusCode.BadRequest, "UnAuthorized");

                var currentUser = db.Managers.FirstOrDefault(man => man.Id == review.ManagerId);
                var applicantLocked = db.Applicants.FirstOrDefault(app => app.Id == review.ApplicantId);
                review.Status = "";

                if (currentUser == null || applicantLocked == null)
                {
                    return BadRequest();
                }

                applicantLocked.LockedBy = currentUser.UserName;

                db.Reviews.Add(review);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new { id = review.Id }, review);

            }
            catch (Exception error)
            {
                return Content(HttpStatusCode.BadRequest, "UnAuthorized - session couldnt be found ");
            }



        }

        // DELETE: api/Reviews/5
        [ResponseType(typeof(Review))]
        public async Task<IHttpActionResult> DeleteReview(int id)
        {
            Review review = await db.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            db.Reviews.Remove(review);
            await db.SaveChangesAsync();

            return Ok(review);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReviewExists(int id)
        {
            return db.Reviews.Count(e => e.Id == id) > 0;
        }
    }
}