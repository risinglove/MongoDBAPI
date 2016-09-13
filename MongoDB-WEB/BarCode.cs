using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;
using System.Net;

namespace MongoDB_WEB
{
    public class BarCode
    {
        #region 生成二维码
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="content">要生成二维码的URL</param>
        /// <param name="level">容错等级</param>
        /// <param name="format">生成的图片格式</param>
        /// <param name="margin">设置间隙</param>
        /// <param name="size">图片大小</param>
        public static void GenQRCode(string content, string level = "M", string format = "png", int margin = 4, int size = 150)
        {
            //string level = "M";   //容错等级
            //string format = "png";  //生成的图片格式
            //int margin = 4;   //二维码区域距图片边缘的距离  设置间隙
            //int size = 100; //图片大小  宽高
            ErrorCorrectionLevel errorCorrectionLevel;
            ImageFormat imgFormat;
            BarcodeFormat barcodeFormat = BarcodeFormat.QR_CODE;
            #region ErrorCorrectionLevel
            switch (level)
            {
                case "L":
                    errorCorrectionLevel = ErrorCorrectionLevel.L;
                    break;
                case "M":
                    errorCorrectionLevel = ErrorCorrectionLevel.M;
                    break;
                case "Q":
                    errorCorrectionLevel = ErrorCorrectionLevel.Q;
                    break;
                case "H":
                    errorCorrectionLevel = ErrorCorrectionLevel.H;
                    break;
                default:
                    errorCorrectionLevel = ErrorCorrectionLevel.M;
                    break;
            }
            #endregion
            #region ImageFormat
            switch (format)
            {
                case "jpeg":
                    imgFormat = ImageFormat.Jpeg;
                    break;
                case "gif":
                    imgFormat = ImageFormat.Gif;
                    break;
                case "bmp":
                    imgFormat = ImageFormat.Bmp;
                    break;
                default:
                    imgFormat = ImageFormat.Png;
                    break;
            }
            #endregion
            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,                      //禁用ECI编码段: use UTF-8 encoding and the ECI segment is omitted
                CharacterSet = "UTF-8",                 //使用UTF-8编码
                Width = size,                           //设置宽度
                Height = size,                          //设置高度
                Margin = margin,                        //设置间隙
                ErrorCorrection = errorCorrectionLevel  //容错等级
            };
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = barcodeFormat;            //采用QR编码
            writer.Options = options;

            //context.Response.Clear();
            //context.Response.ContentType = imgFormat.GetMimeType(); //设置输出流ContentType

            using (Bitmap image = writer.Write(content))    //输出二维码图像
            {
                image.Save("F:/1.png", imgFormat);
            }
            //context.Response.Output.Flush();
            //context.Response.End();
        }
        #endregion

    }
}