namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class Area
    {
        public static Area CreateArea(string name, string code, int id)
        {
            var area = new Area()
            {
                Name = name,
                Code = code,
            };
            if (id != 0)
            {
                area.ParentId = id;
            }
            return area;
        }

        public void Update(string name, string code)
        {
            this.Name = name;
            this.Code = code;
        }
        
        
    }
}