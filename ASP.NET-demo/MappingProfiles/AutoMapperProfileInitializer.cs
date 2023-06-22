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
            CreateMap<RoomMateViewModel, RoomMateModel>();
        }
	}
}

