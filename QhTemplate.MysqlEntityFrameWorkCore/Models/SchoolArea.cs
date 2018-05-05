using System;
using System.Collections.Generic;

namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class SchoolArea
    {
        public int AreaId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Code { get; set; }
        public int Id { get; set; }
        public string Path { get; set; }
        public Area Area { get; set; }
        public IEnumerable<SchoolUser> SchoolUser { get; internal set; }

        public static SchoolArea CreateNewArea(string name,string address,string code,int areaId,string path)
        {
            return new SchoolArea
            {
                AreaId = areaId,
                Name = name,
                Address = address,
                Code = code,
                Path = path
            };
        }


        public void Update(string name, string code,string address)
        {
            this.Name = name;
            this.Code = code;
            this.Address = address;
        }

        public void Migration(int areaId, string path)
        {
            this.AreaId = areaId;
            this.Path = path;
        }
        
    }
}
