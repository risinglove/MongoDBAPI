using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MongoDB.Bson;

namespace BLL
{
    public class UserBLL
    {
        private DAL.UserDAL dal = null;
        public UserBLL()
        {
            dal = new DAL.UserDAL();
        }

        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(User model)
        {
            dal.Add(model);
            return true;
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public List<User> GetAll()
        {
            return dal.GetALL();
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(User model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 根据ObjectID 删除
        /// </summary>
        /// <param name="objId"></param>
        public bool Delete(ObjectId objId)
        {
            return dal.Delete(objId);
        }

        /// <summary>
        /// 根据ObjectID 查询
        /// </summary>
        public User SelectOne(ObjectId objId)
        {
            return dal.SelectOne(objId);
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="model"></param>
        public bool Register(User model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 检验点子邮件是否已存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Check(string name)
        {
            try
            {
                var list = GetAll();
                list = list.Where(u => u.UserName == name).ToList();
                if (list != null && list.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public User Login(string name)
        {
            try
            {
                var list = GetAll();
                list = list.Where(u => u.UserName == name).ToList();
                if (list != null && list.Count == 1)
                {
                    return list[0];
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
        
        public User apiLogin(string appID, string appsecret)
        {
            try
            {
                var list = GetAll();
                list = list.Where(u => u.appID == appID && u.appsecret == appsecret).ToList();
                if (list != null && list.Count == 1)
                {
                    return list[0];
                }
            }
            catch (Exception)
            {
            }
            return null;
        }
        
        public User Login(string name, string paw)
        {
            try
            {
                var list = GetAll();
                list = list.Where(u => u.UserName == name && u.Password == paw).ToList();
                if (list != null && list.Count == 1)
                {
                    return list[0];
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

        public User IdLogin(string Id)
        {
            try
            {
                var list = GetAll();
                list = list.Where(u => u.Id == Id).ToList();
                if (list != null && list.Count == 1)
                {
                    return list[0];
                }
            }
            catch (Exception)
            {
            }
            return null;
        }

    }
}
