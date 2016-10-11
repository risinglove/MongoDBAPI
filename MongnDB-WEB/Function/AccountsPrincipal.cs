using MongnDB_WEB.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace MongnDB_WEB.Function
{
    public class AccountsPrincipal : IPrincipal
    {
        protected IIdentity identity;

        public Model.UsersTable user { get; set; }

        public AccountsPrincipal(Model.UsersTable model)
        {
            identity = new SiteIdentity(model);
            user = model;
        }

        private static BLL.UsersTable userBll
        {
            get
            {
                return new BLL.UsersTable();
            }
        }
        public IIdentity Identity
        {
            get
            {
                return identity;
            }
            set
            {
                identity = value;
            }
        }

        public bool IsInRole(string role)
        {
            return true;
        }

        public static AccountsPrincipal ValidateLogin(string userName, string password)
        {
            var user = userBll.Login(userName, EncyryptionUtil.getMD5(password));
            if (user != null)
                return new AccountsPrincipal(user);
            else
                return null;
        }




    }
}
