using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class ProfileInfo : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string About { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int GraduatedSchoolId { get; set; }
        public int LivingCityId { get; set; }
        public string SocialLinks { get; set; }

    }
}
