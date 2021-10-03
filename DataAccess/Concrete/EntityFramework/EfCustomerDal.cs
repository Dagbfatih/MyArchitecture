using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, SqlContext>, ICustomerDal
    {
        public CustomerDetailsDto GetCustomerDetailsByUser(int userId)
        {
            using (SqlContext context = new SqlContext())
            {
                var result = from c in context.Customers
                             join u in context.Users
                             on c.UserId equals u.Id
                             join r in context.Roles
                             on c.RoleId equals r.Id
                             join l in context.Lessons
                             on c.LessonId equals l.Id
                             where c.UserId == userId
                             select new CustomerDetailsDto
                             {
                                 CustomerDetails = c,
                                 RoleName = r.RoleName,
                                 Email = u.Email,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Status = u.Status,
                                 Lesson = l,
                             };

                return result.FirstOrDefault();
            }
        }
    }
}
