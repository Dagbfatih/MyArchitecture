using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProfileInfoDal : EfEntityRepositoryBase<ProfileInfo, SqlContext>, IProfileInfoDal
    {
        public List<ProfileInfoDetailsDto> GetAllDetails()
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from pI in context.ProfileInfos
                             join u in context.Users
                             on pI.UserId equals u.Id
                             join s in context.Schools
                             on pI.GraduatedSchoolId equals s.Id
                             join c in context.Cities
                             on pI.LivingCityId equals c.Id
                             select new ProfileInfoDetailsDto
                             {
                                 ProfileInfo = pI,
                                 UserName = u.FirstName + u.LastName,
                                 GraduatedSchool = s,
                                 LivingCity = c,
                                 Websites = (from w in context.Websites
                                             where w.UserId == pI.UserId
                                             select new Website
                                             {
                                                 Id = w.Id,
                                                 UserId = w.UserId,
                                                 Address = w.Address
                                             }).ToList()
                             };

                return result.ToList();
            }
        }

        public ProfileInfoDetailsDto GetDetailsByUser(int userId)
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from pI in context.ProfileInfos
                             where pI.UserId == userId
                             select new ProfileInfoDetailsDto
                             {
                                 ProfileInfo = pI,
                                 UserName = (from u in context.Users
                                             where u.Id == pI.UserId
                                             select u.FirstName + " " + u.LastName).FirstOrDefault(),
                                 GraduatedSchool = (from s in context.Schools
                                                    where s.Id == pI.GraduatedSchoolId
                                                    select new School
                                                    {
                                                        Id = s.Id,
                                                        Name = s.Name,
                                                        UserId = s.UserId
                                                    }).FirstOrDefault(),
                                 LivingCity = (from c in context.Cities
                                               where c.Id == pI.LivingCityId
                                               select new City
                                               {
                                                   Id = c.Id,
                                                   Name = c.Name,
                                                   NumberPlate = c.NumberPlate
                                               }).FirstOrDefault(),
                                 Websites = (from w in context.Websites
                                             where w.UserId == pI.UserId
                                             select new Website
                                             {
                                                 Id = w.Id,
                                                 UserId = w.UserId,
                                                 Address = w.Address
                                             }).ToList()
                             };

                return result.FirstOrDefault();
            }
        }
    }
}
