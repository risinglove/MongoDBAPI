using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace BLL
{
    public class Insurace
    {
        private DAL.CurrencyDAL<object> dal = null;
        private string _json;
        public Insurace(string json)
        {
            _json = json;
        }


        public string Ins()
        {
            var result = string.Empty;
            var resultBase = new BaseResult();
            Currency model = null;

            if (string.IsNullOrWhiteSpace(_json))
            {
                resultBase.status = "002"; resultBase.errmsg = "参数不能为空";
                result = JsonConvert.SerializeObject(resultBase);
                LogHelper.WriteLog("WebApi", result);
                return result;
            }
            try
            {
                model = JsonConvert.DeserializeObject<Currency>(_json);
            }
            catch (Exception)
            {
                resultBase.status = "002"; resultBase.errmsg = "Json字符串错误";
                result = JsonConvert.SerializeObject(resultBase);
                LogHelper.WriteLog("WebApi", result);
                return result;
            }


            if (string.IsNullOrWhiteSpace(model.OperationType))
            {
                resultBase.status = "002"; resultBase.errmsg = "操作类型不能为空";
                result = JsonConvert.SerializeObject(resultBase);
                LogHelper.WriteLog("WebApi", result);
                return result;
            }
            if (string.IsNullOrWhiteSpace(model.TableName))
            {
                resultBase.status = "002"; resultBase.errmsg = "表名不能为空";
                result = JsonConvert.SerializeObject(resultBase);
                LogHelper.WriteLog("WebApi", result);
                return result;
            }

            dal = new DAL.CurrencyDAL<object>(model.TableName);
            switch (model.OperationType)
            {
                case "001":  //查询
                    List<object> list = dal.GetALL();
                    resultBase.count = list.Count;
                    resultBase.data = list;
                    resultBase.status = "001";
                    result = JsonConvert.SerializeObject(resultBase);
                    LogHelper.WriteLog("WebApi", result);
                    break;
                case "002":  //添加数据
                    if (dal.Add(model.Data))
                    {
                        resultBase.status = "001";
                        resultBase.msg = "添加成功";
                    }
                    else
                    {
                        resultBase.status = "002";
                        resultBase.errmsg = "添加失败";
                    }
                    result = JsonConvert.SerializeObject(resultBase);
                    LogHelper.WriteLog("WebApi", result);
                    break;
            }
            return result;
        }

    }
}
