using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ASP.NET_demo.Models
{
	public class TaskModel
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, JsonIgnore]
        public string Id { get; set; }

        public string Name { get; set; }

		public bool IsRepeatable { get; set; }

		[JsonIgnore]
		public List<ScheduledTaskModel> ScheduledTasks { get; set; }
	}
}

