using System;
using System.Text.Json.Serialization;

namespace ASP.NET_demo.Models
{
	public class ScheduledTaskModel
	{
		public string Id { get; set; }
		public int Week { get; set; }
		public bool Done { get; set; }

		public string TaskId { get; set; }
		[JsonIgnore]
		public TaskModel Task { get; set; }

		public string RoomMateId { get; set; }
        [JsonIgnore]
        public RoomMateModel RoomMate { get; set; }
	}
}

