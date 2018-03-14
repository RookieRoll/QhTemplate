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
    }
}
