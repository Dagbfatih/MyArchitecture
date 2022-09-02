using Core.Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class ProfileInfoDetailsDto : IDto
    {
        public ProfileInfo ProfileInfo { get; set; }
        public string UserName { get; set; }
        public School GraduatedSchool { get; set; }
        public City LivingCity { get; set; }
        public List<Website> Websites { get; set; }
    }
}
