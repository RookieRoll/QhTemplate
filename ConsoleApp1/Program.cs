using QhTemplate.ApplicationCore.Users;
using QhTemplate.MysqlEntityFrameWorkCore.Models;
using System;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            UserManager manager = new UserManager(new EmsDBContext());
            manager.Finds().ToList().ForEach(m=>Console.WriteLine(m.UserName));
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    
    }
}
