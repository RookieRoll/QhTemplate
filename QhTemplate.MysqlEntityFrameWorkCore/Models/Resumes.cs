using System;

namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class Resumes
    {
        public int Id { get; set; }
        public string RealName { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreationTime { get; set; }
        public int UserId { get; set; }

        public static Resumes Create(string name, int userId)
        {
            Random random=new Random();
            var tempname = DateTime.Now.ToString("yyyyMMddHHmmss") + random.Next(100000, 999999);
            return new Resumes
            {
                RealName = tempname,
                Name = name,
                UserId = userId,
                IsDefault = false,
                CreationTime = DateTime.Now
            };
        }
    }
}