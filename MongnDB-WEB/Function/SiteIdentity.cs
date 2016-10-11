using Microsoft.AspNet.Identity;
using System;
using System.Web;

namespace MongnDB_WEB.Function
{
    [Serializable]
    public class SiteIdentity : System.Security.Principal.IIdentity
    {
        #region 属性
        /// <summary>
        /// ID(用户的唯一标识)
        /// </summary>
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public int uId { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public string AppID { get; set; }

        public string AppSecret { get; set; }
        #endregion

        public string AuthenticationType
        {
            get
            {
                return DefaultAuthenticationTypes.ApplicationCookie;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                var user = HttpContext.Current.Session[Globals.SESSIONKEY_USER] as Model.UsersTable;
                return user != null;
            }
        }

        public string Label { get { return NickName; } }

        public string Name
        {
            get
            {
                return UserName;
            }
        }

        public SiteIdentity(string currentUserName)
        {

        }

        public SiteIdentity(Model.UsersTable model)
        {
            LoadFromDR(model);
        }

        private void LoadFromDR(Model.UsersTable model)
        {
            this.AppID = model.AppID;
            this.AppSecret = model.AppSecret;
            this.CreateDate = model.CreateDate;
            this.uId = model.uId;
            this.UserName = model.UserName;
            this.NickName = model.NickName;
        }

    }
}