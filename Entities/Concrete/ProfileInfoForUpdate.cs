using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class ProfileInfoForUpdate : IEntity
    {
        public int ProfileInfoId { get; set; }
        public string About { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string GraduatedSchool { get; set; }
        public int LivingCityId { get; set; }
        public string SocialLinks { get; set; }

    }
}
