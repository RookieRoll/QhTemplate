namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class Major
    {
        public static Major CreateArea(string name, string code)
        {
            return new Major()
            {
                Name = name,
                Code = code,
            };
        }

        public void Update(string name, string code)
        {
            this.Name = name;
            this.Code = code;
        }
    }
}