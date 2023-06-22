using System;
using ASP.NET_demo.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_demo.DatabaseContext
{
	public class ApiContext : DbContext
	{
        protected readonly IConfiguration Configuration;

        public ApiContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres
            string connectionString = String.Format("Host={0}; Database={1}; Username=={2}; Password=={3}",
                Configuration.GetValue<string>("POSTGRES_HOST"),
                Configuration.GetValue<string>("POSTGRES_DB"),
                Configuration.GetValue<string>("POSTGRES_USER"),
                Configuration.GetValue<string>("POSTGRES_PASSWORD"));
            options.UseNpgsql(connectionString);
        }

        public DbSet<RoomMateModel> RoomMates { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<ScheduledTaskModel> ScheduledTasks { get; set; }
    }
}

