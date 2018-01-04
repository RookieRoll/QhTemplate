using System;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace QhTemplate.ApplicationService.Utils
{
    public class ValidateCodeServiceUtil
    {
        private static string CreateValidateCode(int length)
        {
            const string validateTemplate = "0123456789qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
            var validateStr = new StringBuilder();
            var n = validateTemplate.Length - 1;

            //设置随机值生成器
            var seekSeek = unchecked((int) DateTime.Now.Ticks);
            var seekRand = new Random(seekSeek);

            //生成随机验证码
            for (var i = 0; i < length; i++)
            {
                var beginSeek = seekRand.Next(0, n);
                validateStr.Append(validateTemplate.Substring(beginSeek, 1));
            }

            return validateStr.ToString();
        }

        public static MemoryStream CreateValidateCode(out string code, int length = 4)
        {
            //验证码颜色集合  
            Color[] colors =
            {
                Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan,
                Color.Purple
            };

            //验证码字体集合
            string[] fonts = {"Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体"};

            code = CreateValidateCode(length);
            Bitmap img = new Bitmap((int) code.Length * 18, 32);
            Graphics g = Graphics.FromImage(img); //从Img对象生成新的Graphics对象  
            Random random = new Random();
            //定义图像的大小，生成图像的实例  
            g.Clear(Color.White); //背景设为白色  

            //在随机位置画背景点  
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(img.Width);
                int y = random.Next(img.Height);
                g.DrawRectangle(new Pen(Color.Gray, 0), x, y, 1, 1);
            }

            //验证码绘制在g中  
            for (int i = 0; i < code.Length; i++)
            {
                int cindex = random.Next(7); //随机颜色索引值  
                int findex = random.Next(5); //随机字体索引值  
                Font f = new Font(fonts[findex], 15, FontStyle.Bold); //字体  
                Brush b = new SolidBrush(colors[cindex]); //颜色  
                int flag = 4;
                if ((i + 1) % 2 == 0) //控制验证码不在同一高度  
                {
                    flag = 2;
                }

                g.DrawString(code.Substring(i, 1), f, b, 3 + (i * 12), flag); //绘制一个验证字符  
            }

            MemoryStream ms = new MemoryStream(); //生成内存流对象  
            img.Save(ms, ImageFormat.Jpeg); //将此图像以Png图像文件的格式保存到流中  

            //回收资源  
            g.Dispose();
            img.Dispose();
            return ms;
        }
    }
}