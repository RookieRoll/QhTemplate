using System;
using System.Collections.Generic;

namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class Area
    {
        public Area()
        {
            SchoolArea = new HashSet<SchoolArea>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int ParentId { get; set; }
        public string Path { get; set; }
        public ICollection<SchoolArea> SchoolArea { get; set; }
        
        public static Area Create(string name,int parentId,string code)
        {
            return new Area()
            {
                Name = name,
                ParentId = parentId,
                Code=code
            };
        }
       
    }
}
