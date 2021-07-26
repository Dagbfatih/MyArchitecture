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
                             where c.UserId == userId
                             join u in context.Users
                             on c.UserId equals u.Id
                             join r in context.Roles
                             on c.RoleId equals r.Id
                             select new CustomerDetailsDto
                             {
                                 CustomerId = c.Id,
                                 UserId = c.UserId,
                                 RoleId = c.RoleId,
                                 RoleName = r.RoleName,
                                 Email = u.Email,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Status = u.Status,
                                 IsConfirmed = c.IsConfirmed
                             };

                return result.FirstOrDefault();
            }
        }
    }
}
