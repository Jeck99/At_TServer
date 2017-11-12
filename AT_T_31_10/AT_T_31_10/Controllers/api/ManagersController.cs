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
    public class ManagersController : ApiController
    {
        private AT_T_31_10Context db = new AT_T_31_10Context();

        [HttpGet]
        public  IHttpActionResult GetRecruiters()
        {
            var recruiters = from managers in db.Managers
                            where managers.Role != "Admin"
                            select new
                            {
                                Id = managers.Id,
                                Email = managers.Email,
                                UserName = managers.UserName
                            };
            return Ok(recruiters);
        }





        // GET: api/Managers/5
        [ResponseType(typeof(Manager))]
        public async Task<IHttpActionResult> GetManager(int id)
        {
            Manager manager = await db.Managers.FindAsync(id);
            if (manager == null)
            {
                return NotFound();
            }

            return Ok(manager);
        }



        // POST: api/Managers
        [ResponseType(typeof(Manager))]
        public async Task<IHttpActionResult> PostManager(Manager manager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Managers.Add(manager);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = manager.Id }, manager);
        }

        //// DELETE: api/Managers/5
        //[ResponseType(typeof(Manager))]
        //public async Task<IHttpActionResult> LogOf(int id)
        //{
        //    //LoginCustomers Result = DB.LoginCustomers.FirstOrDefault(item => item.SessionId == session);
        //    //if (Result == null)
        //    //{
        //    //    return Content(HttpStatusCode.BadRequest, "There some issues with your logout , Check if you Logged in");
        //    //}
        //    //manager.RemoveSessionID(HttpContext.Current);
        //    //DB.LoginCustomers.Remove(Result);
        //    //DB.SaveChanges();

        //    //return Ok(Result);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ManagerExists(int id)
        {
            return db.Managers.Count(e => e.Id == id) > 0;
        }




        // PUT: api/Managers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutManager(int id, Manager manager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != manager.Id)
            {
                return BadRequest();
            }

            db.Entry(manager).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManagerExists(id))
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
    }
}