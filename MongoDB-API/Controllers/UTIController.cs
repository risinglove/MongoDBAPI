using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Utility;

namespace API.Controllers
{
    public class UTIController : ApiController
    {
        private string FlieServiceUrl => ConfigurationManager.AppSettings["FlieServiceURL"];
        
        #region 获取图形验证码
        /// <summary>
        /// 获取图形验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Verfy()
        {
            string code;
            byte[] by = Helper.CreateValidateGraphic(out code, 4, 100, 30, 20);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new ByteArrayContent(by);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            return result;
        } 
        #endregion

        #region 文件上传
        /// <summary>
        /// 文件上传(未限定文件类型)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<Dictionary<string, string>>> FileUpload()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            string pata = "/File/Whole/" + DateTime.Now.ToString("yyyyMMdd");
            LogHelper.WriteLog("pata : " + pata, "UTIController.FileUpload");
            string root = HttpContext.Current.Server.MapPath("/" + pata);//指定要将文件存入的服务器物理位置  
            LogHelper.WriteLog("root: " + root, "UTIController.FileUpload");
            if (!Directory.Exists(root))//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(root);
            }
            var provider = new MultipartFormDataMemoryStreamProvider();
            try
            {
                // Read the form data.  
                await Request.Content.ReadAsMultipartAsync(provider);
                LogHelper.WriteLog("Read the form data", "UTIController.FileUpload");
                // This illustrates how to get the file names.
                foreach (var fileContent in provider.FileContents)
                {
                    LogHelper.WriteLog("This illustrates how to get the file names.", "UTIController.FileUpload");
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    string fileName = fileContent.Headers.ContentDisposition.FileName.Replace("\"", "");
                    dic.Add("OldFileName", fileName);
                    string Ext = Path.GetExtension(fileName);
                    dic.Add("ExtName", Ext);
                    fileName = EncyryptionUtil.GetMd5Utf8(EncyryptionUtil.AESEncrypt(Guid.NewGuid().ToString())) + Ext;
                    dic.Add("NewFileName", fileName);
                    dic.Add("FilePath", FlieServiceUrl + pata + "/" + fileName);
                    var stream = await fileContent.ReadAsStreamAsync();
                    using (StreamWriter sw = new StreamWriter(Path.Combine(root, fileName)))
                    {
                        stream.CopyTo(sw.BaseStream);
                        sw.Flush();
                        list.Add(dic);
                    }
                }
                //TODO:这样做直接就将文件存到了指定目录下，暂时不知道如何实现只接收文件数据流但并不保存至服务器的目录下，由开发自行指定如何存储，比如通过服务存到图片服务器  
                //foreach (var key in provider.FormData.AllKeys)
                //{//接收FormData  
                //    dic.Add(key, provider.FormData[key]);
                //}
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e.Message);
                throw;
            }
            return list;
        }


        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<Dictionary<string, string>>> ImgUpload()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            string pata = "/File/uploadpic/" + DateTime.Now.ToString("yyyyMMdd");
            LogHelper.WriteLog("pata : " + pata, "UTIController.FileUpload");
            string root = HttpContext.Current.Server.MapPath(pata+ "/original");//指定要将文件存入的服务器物理位置  
            LogHelper.WriteLog("root: " + root, "UTIController.FileUpload");
            if (!Directory.Exists(root))//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(root);
            }
            var provider = new MultipartFormDataMemoryStreamProvider();
            try
            {
                // Read the form data.  
                await Request.Content.ReadAsMultipartAsync(provider);
                LogHelper.WriteLog("Read the form data", "UTIController.FileUpload");
                // This illustrates how to get the file names.
                foreach (var fileContent in provider.FileContents)
                {
                    LogHelper.WriteLog("This illustrates how to get the file names.", "UTIController.FileUpload");
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    string fileName = fileContent.Headers.ContentDisposition.FileName.Replace("\"", "");
                    dic.Add("OldFileName", fileName);
                    string Ext = Path.GetExtension(fileName).ToLowerInvariant();
                    if (Ext == ".png" || Ext == ".jpg" || Ext == ".jpeg")
                    {
                        dic.Add("ExtName", Ext);
                        fileName = EncyryptionUtil.GetMd5Utf8(EncyryptionUtil.AESEncrypt(Guid.NewGuid().ToString())) + Ext;
                        dic.Add("NewFileName", fileName);
                        dic.Add("FilePath", FlieServiceUrl + pata + "/original/" + fileName);
                        var stream = await fileContent.ReadAsStreamAsync();
                        using (StreamWriter sw = new StreamWriter(Path.Combine(root, fileName)))
                        {
                            dic.Add("status", "1");
                            stream.CopyTo(sw.BaseStream);
                            sw.Flush();
                        }
                        PicHelper.PicZoomAuto(HttpContext.Current.Server.MapPath(pata + "/original/" + fileName), HttpContext.Current.Server.MapPath(pata), fileName);
                    }
                    else
                    {
                        dic.Add("status", "0");
                        dic.Add("errmsg", "格式不正确");
                    }
                    list.Add(dic);
                }
                //TODO:这样做直接就将文件存到了指定目录下，暂时不知道如何实现只接收文件数据流但并不保存至服务器的目录下，由开发自行指定如何存储，比如通过服务存到图片服务器  
                //foreach (var key in provider.FormData.AllKeys)
                //{//接收FormData  
                //    dic.Add(key, provider.FormData[key]);
                //}
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e.Message);
                throw;
            }
            return list;
        }


        #endregion
    }


}
