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
using System.Web;
using AT_T_31_10.Models;
using System.Web.SessionState;
using AT_T_31_10.Utils;

namespace AT_T_31_10.Controllers.api
{
    public class ManagerLoginsController : ApiController
    {
        private AT_T_31_10Context db = new AT_T_31_10Context();
        SessionIDManager manager = new SessionIDManager();
        private Encryption Enc = new Encryption();
        private Security Sec = new Security();


        [HttpPost]
        public IHttpActionResult Login(string email, string password)
        {
            //// האם קיים ברשימת משתמשים ?
            var Result = db.Managers.FirstOrDefault(user => user.Email == email);
            if (Result == null)
                return Content(HttpStatusCode.BadRequest, "Incorrect Pasword/User Input");

            if (string.IsNullOrEmpty(password) || String.IsNullOrWhiteSpace(password))
                return Content(HttpStatusCode.BadRequest, "password required");


            if (!Enc.ValidatePassword(password, Result.Password))
                return Content(HttpStatusCode.BadRequest, "Incorrect  Pasword/User Input");

            ManagerLogins us = new ManagerLogins();
            us.SessionId = manager.CreateSessionID(HttpContext.Current);
            bool redirected = false;
            bool isAdded = false;
            manager.SaveSessionID(HttpContext.Current, us.SessionId, out redirected, out isAdded);
            us.UserId = Result.Id;
            db.ManagerLogins.Add(us);
            db.SaveChanges();

            CreatedAtRoute("DefaultApi", new { id = us.Id }, us);
            return Ok(us);
        }

        // GET: api/ManagerLogins/5
        [ResponseType(typeof(ManagerLogins))]
        public async Task<IHttpActionResult> GetManagerLogins(long id)
        {
            ManagerLogins managerLogins = await db.ManagerLogins.FindAsync(id);
            if (managerLogins == null)
                return NotFound();

            return Ok(managerLogins);
        }

        [HttpDelete]
        public  IHttpActionResult LogOff(string session)
        {
            ManagerLogins managerLogins =  db.ManagerLogins.FirstOrDefault(item => item.SessionId == session);
            if (managerLogins == null)
             return NotFound();

            manager.RemoveSessionID(HttpContext.Current);
            db.ManagerLogins.Remove(managerLogins);
            db.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public bool RoleState(string session)
        {

                if (Sec.IsAdmin(session))
                    return true;

                return false;
  
        }


        [HttpPost]
        public async Task<IHttpActionResult> PostManagerLogins(Manager managerToAdd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // האם האימייל קיים ?
            if (db.Managers.FirstOrDefault(item => item.Email == managerToAdd.Email) != null)
                return Content(HttpStatusCode.BadRequest, "This User Is Already Exist - server");

            Manager Manager = new Manager();
            Manager.UserName = managerToAdd.UserName;

            Manager.Email = managerToAdd.Email;
            Manager.Password = Enc.CreateHash(managerToAdd.Password);
            Manager.Role = "";
            db.Managers.Add(Manager);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = managerToAdd.Id }, managerToAdd);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }





    }
}