﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace Utility
{
   public class Helper
    {
        /// <summary>
        /// 產生圖形驗證碼。
        /// </summary>
        /// <param name="Code">傳出驗證碼。</param>
        /// <param name="CodeLength">驗證碼字元數。</param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="FontSize"></param>
        /// <returns></returns>
        public static byte[] CreateValidateGraphic(out String Code, int CodeLength, int Width, int Height, int FontSize)
        {
            String sCode = String.Empty;
            //顏色列表，用於驗證碼、噪線、噪點
            Color[] oColors ={
             System.Drawing.Color.Black,
             System.Drawing.Color.Red,
             System.Drawing.Color.Blue,
             System.Drawing.Color.Green,
             System.Drawing.Color.Orange,
             System.Drawing.Color.Brown,
             System.Drawing.Color.Brown,
             System.Drawing.Color.DarkBlue
            };
            //字體列表，用於驗證碼
            string[] oFontNames = { "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };
            //驗證碼的字元集，去掉了一些容易混淆的字元
            char[] oCharacter = {
       '2','3','4','5','6','8','9',
       'A','B','C','D','E','F','G','H','J','K', 'L','M','N','P','R','S','T','W','X','Y'
      };
            Random oRnd = new Random();
            Bitmap oBmp = null;
            Graphics oGraphics = null;
            int N1 = 0;
            System.Drawing.Point oPoint1 = default(System.Drawing.Point);
            System.Drawing.Point oPoint2 = default(System.Drawing.Point);
            string sFontName = null;
            Font oFont = null;
            Color oColor = default(Color);

            //生成驗證碼字串
            for (N1 = 0; N1 <= CodeLength - 1; N1++)
            {
                sCode += oCharacter[oRnd.Next(oCharacter.Length)];
            }

            oBmp = new Bitmap(Width, Height);
            oGraphics = Graphics.FromImage(oBmp);
            oGraphics.Clear(System.Drawing.Color.White);
            try
            {
                for (N1 = 0; N1 <= 4; N1++)
                {
                    //畫噪線
                    oPoint1.X = oRnd.Next(Width);
                    oPoint1.Y = oRnd.Next(Height);
                    oPoint2.X = oRnd.Next(Width);
                    oPoint2.Y = oRnd.Next(Height);
                    oColor = oColors[oRnd.Next(oColors.Length)];
                    oGraphics.DrawLine(new Pen(oColor), oPoint1, oPoint2);
                }

                float spaceWith = 0, dotX = 0, dotY = 0;
                if (CodeLength != 0)
                {
                    spaceWith = (Width - FontSize * CodeLength - 10) / CodeLength;
                }

                for (N1 = 0; N1 <= sCode.Length - 1; N1++)
                {
                    //畫驗證碼字串
                    sFontName = oFontNames[oRnd.Next(oFontNames.Length)];
                    oFont = new Font(sFontName, FontSize, FontStyle.Italic);
                    oColor = oColors[oRnd.Next(oColors.Length)];

                    dotY = (Height - oFont.Height) / 2 + 2;//中心下移2像素
                    dotX = Convert.ToSingle(N1) * FontSize + (N1 + 1) * spaceWith;

                    oGraphics.DrawString(sCode[N1].ToString(), oFont, new SolidBrush(oColor), dotX, dotY);
                }

                for (int i = 0; i <= 30; i++)
                {
                    //畫噪點
                    int x = oRnd.Next(oBmp.Width);
                    int y = oRnd.Next(oBmp.Height);
                    Color clr = oColors[oRnd.Next(oColors.Length)];
                    oBmp.SetPixel(x, y, clr);
                }

                Code = sCode;
                //保存图片数据
                using (MemoryStream stream = new MemoryStream())
                {
                    oBmp.Save(stream, ImageFormat.Jpeg);
                    //输出图片流
                    return stream.ToArray();
                }
            }
            finally
            {
                oGraphics.Dispose();
            }
        }


        #region 生成QRCode
        public static byte[] GenQRCode(string content)
        {
            string level = "M";
            string format = "png";
            int margin = 4;
            int size = 100;
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
            using (Bitmap image = writer.Write(content))    //输出二维码图像
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, imgFormat);
                    return ms.ToArray();
                }
            }
        }
        #endregion

        #region 将请求转还为BarCode类型  目前只加了二维码和一维条形码的一种
        public static BarcodeFormat CodeFormat(string type)
        {
            switch (type)
            {
                case "QR_CODE":
                    return ZXing.BarcodeFormat.QR_CODE;
                case "CODE_128":
                    return ZXing.BarcodeFormat.CODE_128;
                default:
                    return ZXing.BarcodeFormat.CODABAR;
            }
        }
        #endregion



    }
}