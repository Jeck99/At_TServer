using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AT_T_31_10.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set;}
        public string Role { get; set; }

    }
}