using System;
using ASP.NET_demo.Models;
using ASP.NET_demo.ViewModels;
using AutoMapper;

namespace ASP.NET_demo.MappingProfiles
{
	public class AutoMapperProfileInitializer : Profile
	{
		public AutoMapperProfileInitializer()
		{
			CreateMap<RoomMateModel, RoomMateViewModel>();
            CreateMap<RoomMateViewModel, RoomMateModel>()
                .ForMember(dest => dest.ScheduledTasks, opt => opt.Ignore());

            CreateMap<TaskModel, TaskViewModel>();
            CreateMap<TaskViewModel, TaskModel>()
                .ForMember(dest => dest.ScheduledTasks, opt => opt.Ignore());

            CreateMap<ScheduledTaskModel, ScheduledTaskViewModel>()
                .ForMember(dest => dest.RoomMateInCharge, opt => opt.MapFrom(src => src.RoomMate.Name))
                .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.Task.Name));
            CreateMap<ScheduledTaskViewModel, ScheduledTaskModel>()
                .ForMember(dest => dest.RoomMateId, opt => opt.Ignore())
                .ForMember(dest => dest.TaskId, opt => opt.Ignore());
        }
	}
}

