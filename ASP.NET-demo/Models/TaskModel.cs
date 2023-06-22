using System;
namespace ASP.NET_demo.Models
{
	public class TaskModel
	{
        public string Id { get; set; }

        public string Name { get; set; }

		public bool IsRepeatable { get; set; }

		public List<ScheduledTaskModel> ScheduledTasks { get; set; }
	}
}

