using System;
namespace ASP.NET_demo.Models
{
	public class RoomMateModel
	{
        public string Id { get; set; }

        public string Name { get; set; }

        public List<ScheduledTaskModel> ScheduledTasks { get; set; }
    }
}

