using System;
namespace ASP.NET_demo.ViewModels
{
	public class ScheduledTaskViewModel
	{
        public string Id { get; set; }
        public int Week { get; set; }
        public bool Done { get; set; }

        public string TaskName { get; set; }
        public string RoomMateInCharge { get; set; }
    }
}

