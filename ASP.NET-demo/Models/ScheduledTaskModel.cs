using System;
namespace ASP.NET_demo.Models
{
	public class ScheduledTaskModel
	{
		public string Id { get; set; }
		public int Week { get; set; }
		public bool Done { get; set; }

		public string TaskId { get; set; }
		public TaskModel Task { get; set; }

		public string RoomMateId { get; set; }
		public RoomMateModel RoomMate { get; set; }
	}
}

