using Core.Entities.Concrete;
using Core.Utilities.IoC;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Concrete.EntityFramework
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
        public DbSet<Question> Questions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestQuestion> TestQuestions { get; set; }
        public DbSet<QuestionTicket> QuestionTickets { get; set; }
        public DbSet<TestTicket> TestTickets { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ProfileImage> ProfileImages { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Translate> Translates { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<QuestionResult> QuestionResults { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<GradeLevel> GradeLevels { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ProfileInfo> ProfileInfos { get; set; }
        public DbSet<ProfileInfoWebsite> ProfileInfoWebsites { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Website> Websites { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }

    }
}
