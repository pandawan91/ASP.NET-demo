using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ASP.NET_demo.Models
{
	public class ScheduledTaskModel
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
		public int Week { get; set; }
        [JsonIgnore]
        public bool Done { get; set; }

		public string TaskId { get; set; }
		[JsonIgnore]
		public TaskModel Task { get; set; }

		public string RoomMateId { get; set; }
        [JsonIgnore]
        public RoomMateModel RoomMate { get; set; }
	}
}

