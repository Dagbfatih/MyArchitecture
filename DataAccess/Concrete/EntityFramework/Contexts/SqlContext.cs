using Core.Entities.Concrete;
using Core.Utilities.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class SqlContext : DbContext
    {
        public SqlContext()
        {
            Configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("MsSql"));
            //optionsBuilder.UseSqlServer(Configuration.GetConnectionString("MsSqlForPublish"));
        }

        protected IConfiguration Configuration { get; }
        
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }      
        public DbSet<Language> Languages { get; set; }
        public DbSet<Translate> Translates { get; set; }       
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
