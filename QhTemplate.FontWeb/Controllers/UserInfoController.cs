﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using IronPython.Modules;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using QhTemplate.ApplicationCore.Exceptions;
using QhTemplate.FontWeb.Models.UserInfo;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.FontWeb.Controllers
{
    public class UserInfoController : Controller
    {
        private readonly EmsDBContext _context;
        private readonly IHostingEnvironment _environment;
        private readonly string Path;

        public UserInfoController(EmsDBContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
            Path = _environment.WebRootPath + "/upload/resumes/";
        }

        public IActionResult Index()
        {
            var userId = HttpContext.User.Claims.SingleOrDefault(x => x.Type.Equals(ClaimTypes.Sid))?.Value;
            var ids = int.Parse(userId);
            var model = new ShowInfoViewModel
            {
                UserInfos = GetLoginUserInfo(ids)
            };
            var path = Path + ids + ".pdf";
            if (System.IO.File.Exists(path))
            {
                model.HasResume = true;
                model.ResumeInfos = GetResumeInfo(ids);
            }
            else
            {
                model.HasResume = true;
                model.ResumeInfos = null;
            }
            return View(model);
        }

        private UserInfo GetLoginUserInfo(int ids)
        {
            var user = (from users in _context.User
                join relation in _context.SchoolUser on users.Id equals relation.UserId
                join schools in _context.SchoolArea on relation.SchoolId equals schools.Id
                where users.UserType == (int) UserType.Student && users.Id == ids
                select new UserInfo
                {
                    Id = users.Id,
                    School = schools.Name,
                    UserName = users.UserName,
                    Name = users.Name,
                    Email = users.EmailAddress
                }).FirstOrDefault();
            return user ?? throw new UserFriendlyException("该用户信息不存在");
        }

        private ResumeInfo GetResumeInfo(int Ids)
        {
            var path = Path + Ids + ".pdf";
            FileInfo file = new FileInfo(path);
            ResumeInfo info = new ResumeInfo
            {
                Name = file.Name,
                Time = file.CreationTime.ToString("yyyy-MM-dd"),
                Url = path
            };
            return info;
        }
    }
}