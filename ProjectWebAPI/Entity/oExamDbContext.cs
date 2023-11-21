using Microsoft.EntityFrameworkCore;
using AutoMapper;
using ProjectWebAPI.Entity;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

namespace ProjectWebAPI.Entity
{
    public class oExamDbContext : DbContext
    {
        //define entity set
        private readonly IConfiguration configuration;

        public oExamDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<QuestionBank> QuestionBanks { get; set; }
        public DbSet<TestStructure> TestStructures { get; set; }
        public DbSet<AssignedTest> AssignedTests { get; set; }
        public DbSet<UserResponse> UserResponses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("server=DESKTOP-4O1D65I\\SQLEXPRESS;database=EComm11DB;trusted_connection=true;TrustServerCertificate=True");
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyConnection"));
        }
    }
}


