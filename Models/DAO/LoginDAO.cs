using Models.EF;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
   public class LoginDAO
    {
        web_interiorEntities db = new web_interiorEntities();
        public bool Check(LoginModel acc)
        {
            var checkLogin = db.Users.Any(s => s.Name == acc.UserLogin && s.PassWordHash == acc.Password);
            return checkLogin;
        }

    }
}
