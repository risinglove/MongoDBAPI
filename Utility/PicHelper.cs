using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Utility
{
    public class PicHelper
    {

        /// <summary>
        /// /
        /// </summary>
        /// <param name="initpath">原图</param>
        /// <param name="savepath">缩略图存放地址</param>
        public static void PicZoomAuto(string initpath, string pata, string name)
        {
            Task.Run(() =>
            {
                Thread.Sleep(2000);
                Task.Run(() =>
                {
                    zoomauto(initpath, pata + "/150/" + name, 150, 150);
                });

                Task.Run(() =>
                {
                    zoomauto(initpath, pata + "/400/" + name, 400, 400);
                });

                Task.Run(() =>
                {
                    zoomauto(initpath, pata + "/600/" + name, 600, 600);

                });
                Task.Run(() =>
                {
                    zoomauto(initpath, pata + "/1200/" + name, 1200, 1200);

                });
            });
        }




        #region 图片等比缩放
        /// <summary>
        /// 图片等比缩放
        /// </summary>
        /// <param name="postedfile">原图</param>
        /// <param name="savepath">缩略图存放地址</param>
        /// <param name="targetwidth">指定的最大宽度</param>
        /// <param name="targetheight">指定的最大高度</param>
        public static void zoomauto(string initpath, string savepath, double targetwidth, double targetheight)
        {
            try
            {
                //创建目录
                string dir = Path.GetDirectoryName(savepath);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                //原始图片（获取原始图片创建对象，并使用流中嵌入的颜色管理信息）
                Image initimage = Image.FromFile(initpath);

                //原图宽高均小于模版，不作处理，直接保存
                if (initimage.Width <= targetwidth && initimage.Height <= targetheight)
                {
                    //保存
                    initimage.Save(savepath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else
                {
                    //缩略图宽、高计算
                    double newwidth = initimage.Width;
                    double newheight = initimage.Height;

                    //宽大于高或宽等于高（横图或正方）
                    if (initimage.Width > initimage.Height || initimage.Width == initimage.Height)
                    {
                        //如果宽大于模版
                        if (initimage.Width > targetwidth)
                        {
                            //宽按模版，高按比例缩放
                            newwidth = targetwidth;
                            newheight = initimage.Height * (targetwidth / initimage.Width);
                        }
                    }
                    //高大于宽（竖图）
                    else
                    {
                        //如果高大于模版
                        if (initimage.Height > targetheight)
                        {
                            //高按模版，宽按比例缩放
                            newheight = targetheight;
                            newwidth = initimage.Width * (targetheight / initimage.Height);
                        }
                    }

                    //生成新图
                    //新建一个bmp图片
                    Image newimage = new Bitmap((int)newwidth, (int)newheight);
                    //新建一个画板
                    Graphics newg = Graphics.FromImage(newimage);

                    //设置质量
                    newg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    newg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    //置背景色
                    newg.Clear(Color.Transparent);
                    //画图
                    newg.DrawImage(initimage, new Rectangle(0, 0, newimage.Width, newimage.Height), new Rectangle(0, 0, initimage.Width, initimage.Height), GraphicsUnit.Pixel);

                    //保存缩略图
                    newimage.Save(savepath, System.Drawing.Imaging.ImageFormat.Png);

                    //释放资源
                    newg.Dispose();
                    newimage.Dispose();
                    initimage.Dispose();
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e.Message, "PicHelper.zoomauto");
                throw;
            }
        }
        #endregion

    }
}
