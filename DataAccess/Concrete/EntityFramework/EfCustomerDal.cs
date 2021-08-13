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
                             where c.UserId == userId
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
                                 IsConfirmed = c.IsConfirmed,
                                 Branch = c.BranchId == 0 ? new Branch
                                 {
                                     Id = 0,
                                     Name = ""
                                 } : (from b in context.Branches
                                      where c.BranchId == b.Id
                                      select new Branch
                                      {
                                          Id = b.Id,
                                          Name = b.Name
                                      }).FirstOrDefault()
                             };

                return result.FirstOrDefault();
            }
        }
    }
}
