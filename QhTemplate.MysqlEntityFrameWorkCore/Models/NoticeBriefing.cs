namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class NoticeBriefing
    {
        public int Id { get; set; }
        public int BriefingId { get; set; }
        public int UserId { get; set; }

        public BriefingContent Briefing { get; set; }
        public User User { get; set; }

    }
}