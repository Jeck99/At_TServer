using AT_T_31_10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AT_T_31_10.Utils
{
    public class Security 
    {

        private AT_T_31_10Context DataBase = new AT_T_31_10Context();


        public bool AuthSecure(string head)
        {
                var Result = DataBase.ManagerLogins.FirstOrDefault(Loged => Loged.SessionId == head);
                if (Result == null)
                    return false;

                return true;
        }


        public  bool IsAdmin(string head)
        {
            var Result = DataBase.ManagerLogins.FirstOrDefault(Loged => Loged.SessionId == head);
            if(Result == null)
                return false;
            
                return DataBase.Managers.FirstOrDefault(User => User.Id == Result.UserId).Role == "Admin";
        }

        public bool IsLogged(string head)
        {
            bool IsManagerLogged = DataBase.ManagerLogins.Any(Loged => Loged.SessionId == head);
            if (IsManagerLogged)
                return true;
            
            return false;
        }

        public int GetId(string head)
        {
            return DataBase.ManagerLogins.FirstOrDefault(Loged => Loged.SessionId == head).UserId;        
        }

        //internal bool AuthSecure(object head)
        //{
        //    throw new NotImplementedException();
        //}
    }
}