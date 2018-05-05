using System;
using Community.CsharpSqlite;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.ViewModels.BriefingContents
{
    public class EditBriefContentViewModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Held { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string StartTime { get; set; }

        public static EditBriefContentViewModel ConvertToEditViewModel(BriefingContent obj)
        {
            return new EditBriefContentViewModel
            {
                Id =obj.Id,
                CompanyName = obj.CompanyName,
                Held = obj.Held,
                Title = obj.Title,
                Content = obj.Content
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                ,
                StartTime = obj.StartTime.ToString("yyyy-MM-ddTHH:mm")
            };
        }
    }
}