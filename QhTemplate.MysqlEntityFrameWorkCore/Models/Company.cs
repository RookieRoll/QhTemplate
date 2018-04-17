﻿using System;
using System.Collections.Generic;

namespace QhTemplate.MysqlEntityFrameWorkCore.Models
{
    public partial class Company
    {
        public Company()
        {
            CompanyUser = new HashSet<CompanyUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime CreateTime { get; set; }
        public string LegalPerson { get; set; }
        public string Tellphone { get; set; }
        public string Description { get; set; }

        public ICollection<CompanyUser> CompanyUser { get; set; }

        public static Company Create(string name, string address, string LegalPerson, string tellphone)
        {
            return new Company()
            {
                Name = name,
                Address = address,
                CreateTime = DateTime.Now,
                LegalPerson = LegalPerson,
                Tellphone = tellphone
            };
        }

        public void Update(string name, string address, string tellphone)
        {
            this.Name = name;
            this.Address = address;
            this.Tellphone = tellphone;
        }
    }
}