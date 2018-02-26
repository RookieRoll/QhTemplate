using QhTemplate.ApplicationCore.Users;
using QhTemplate.MysqlEntityFrameWorkCore.Models;
using System;
using System.Linq;
using System.Threading;
using QhTemplate.ApplicationService.Utils.EmailUtils;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
//            UserManager manager = new UserManager(new EmsDBContext());
//            manager.Finds().ToList().ForEach(m=>Console.WriteLine(m.UserName));
            EmailHelper email = new EmailHelper();
            EmailRecevierConfig config = new EmailRecevierConfig()
            {
                EmailAdd = "1437711344@qq.com",
                Subject = "测试",
                Content = "测试233333",
                Addressee = "笨小孩2333",
                FilePath = @"D:\测试数据\散文.txt"
            };
            var p=email.SendEmailAsync(config);
            while (true)
            {
                if (p.IsCompletedSuccessfully)
                {
                    Console.WriteLine("ok");
                    break;
                }
                Thread.Sleep(1000);
            }
         
            Console.ReadKey();
        }
    }
}