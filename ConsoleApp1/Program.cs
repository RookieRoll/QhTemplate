using QhTemplate.MysqlEntityFrameWorkCore.Models;
using System;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (EmsDBContext db = new EmsDBContext())
            {
                var temp = db.User.ToList();

            }
            Console.WriteLine("Hello World!");
        }
    
    }
}
