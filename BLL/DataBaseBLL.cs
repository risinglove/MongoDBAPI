using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using ViewModel;

namespace BLL
{
    public class DataBaseBLL
    {
        private DAL.DataBaseDAL dal = null;
        public DataBaseBLL()
        {
            dal = new DAL.DataBaseDAL();
        }

        public void GenerateUserDLL(string UserId)
        {
            var list = dal.Select(UserId);
            ModelHelper.CreateUserDll(UserId, list);
        }


        /// <summary>
        /// 根据用户ID查询
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<DataBaseModel> Select(string UserId)
        {
            var list = dal.Select(UserId);
            return list;
        }

    }
}
