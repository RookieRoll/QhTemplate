using Pomelo.AspNetCore.TimedJob;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using QhTemplate.ApplicationService.Utils.EmailUtils;
using QhTemplate.FontWeb.Models.NoticeBrefing;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.FontWeb.Service
{
    public class TimedJobServices
    {
        private readonly EmsDBContext _context;
        private readonly IHostingEnvironment _environment;

        public TimedJobServices(EmsDBContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [Invoke(Begin = "2018-5-28 7:00", SkipWhileExecuting = true)]
        public void Run()
        {
            var template = GetEmailTemplate();
            var noticelist = GetNotices().Where(m => m.Briefings.Count != 0).ToList();
            if (noticelist.Count == 0)
                return;
            var part = Partitioner.Create(0, noticelist.Count());
            Parallel.ForEach(part, async item =>
            {
                for (var i = item.Item1; i < item.Item2; i++)
                {
                    var tempValue = noticelist[i];
                    EmailRecevierConfig config = new EmailRecevierConfig
                    {
                        Addressee = tempValue.Name,
                        EmailAdd = tempValue.Name,
                        Subject = "宣讲会提醒-就业网",
                        Content = GetEmailContent(template, tempValue.Briefings)
                    };

                    EmailHelper helper = new EmailHelper();
                    await helper.SendEmailAsync(config);
                }
            });
        }

        private string GetEmailContent(string template, List<BriefingMessage> list)
        {
            var html = "";
            list.ForEach(m => { html += $"<tr><td>{m.Company}</td><td>{m.Held}</td><td>{m.Time}</td></tr>"; });
            var result = string.Format(template, html);
            return result;
        }

        private string GetEmailTemplate()
        {
            var path = _environment.WebRootPath + "/template/noticehtml.tpl";
            string html;
            using (var reader = new StreamReader(File.OpenRead(path)))
            {
                html = reader.ReadToEnd();
            }

            return html;
        }

        private List<Notice> GetNotices()
        {
            var temp1 = _context.NoticeBriefing.ToList();
            var resule = (from notices in _context.NoticeBriefing
                    join briefings in _context.BriefingContent on notices.BriefingId equals briefings.Id
                    join users in _context.User on notices.UserId equals users.Id
                    join schools in _context.SchoolArea on briefings.SchoolId equals schools.Id
                    group new
                    {
                        notices,
                        users,
                        briefings,
                        schools
                    } by notices.UserId
                    into temp
                    select new Notice
                    {
                        UserId = temp.FirstOrDefault().users.Id,
                        Email = temp.FirstOrDefault().users.EmailAddress,
                        Name = temp.FirstOrDefault().users.Name,
                        Briefings = temp.Select(m => new BriefingMessage
                        {
                            Company = m.briefings.CompanyName,
                            Id = m.briefings.Id,
                            Held = m.schools.Name + m.briefings.Held,
                            Time = m.briefings.StartTime.ToString("MM-dd HH:mm")
                        }).ToList()
                    }
                ).ToList();
            return resule;
        }
    }
}