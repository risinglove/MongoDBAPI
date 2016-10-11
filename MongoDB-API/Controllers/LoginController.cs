using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Bson;
using static System.String;
using ViewModel;
using BLL;

namespace API.Controllers
{
    public class LoginController : ApiController
    {
        public List<Model.UsersTable> GetUserModel()
        {
            return new BLL.UsersTable().GetModelList("");
        }
    }
    

}
