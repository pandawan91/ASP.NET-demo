using System;
using ASP.NET_demo.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_demo.DatabaseContext
{
	public class ApiContext : DbContext
	{
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        public DbSet<RoomMateModel> RoomMates { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<ScheduledTaskModel> ScheduledTasks { get; set; }
    }
}

