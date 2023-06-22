using System;
using ASP.NET_demo.DatabaseContext;
using ASP.NET_demo.Models;
using ASP.NET_demo.ViewModels;
using AutoMapper;

namespace ASP.NET_demo.Services
{
	public class RoomMateService
	{
		private readonly ApiContext _apiContext;
        private readonly IMapper _mapper;

        public RoomMateService(ApiContext apiContext, IMapper mapper)
		{
			_apiContext = apiContext
				?? throw new ArgumentNullException(nameof(apiContext));
			_mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

		public async Task CreateRoomMateAsync(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
				throw new ArgumentNullException(nameof(name));

            await _apiContext.RoomMates.AddAsync(new RoomMateModel()
			{
				Name = name
			});

			await _apiContext.SaveChangesAsync();
		}

        public async Task<RoomMateViewModel> FindRoomMateAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            var roomMate = await _apiContext.RoomMates.FindAsync(id)
                ?? throw new KeyNotFoundException(nameof(id));

			return _mapper.Map<RoomMateViewModel>(roomMate);
        }

        public List<RoomMateViewModel> FindAllRoomMates()
        {
            return _apiContext
                .RoomMates
                .Select(x => _mapper.Map<RoomMateViewModel>(x))
                .ToList();
        }

        public async Task UpdateRoomMateAsync(RoomMateViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var roomMate = await _apiContext.RoomMates.FindAsync(model.Id)
                ?? throw new KeyNotFoundException(nameof(model));
            _apiContext.RoomMates.Update(_mapper.Map<RoomMateModel>(model));

            await _apiContext.SaveChangesAsync();
        }

        public async Task DeleteRoomMateAsync(string id)
		{
			if (string.IsNullOrWhiteSpace(id))
				throw new ArgumentNullException(nameof(id));

			var roomMate = await _apiContext.RoomMates.FindAsync(id)
				?? throw new KeyNotFoundException(nameof(id));

			_apiContext.RoomMates.Remove(roomMate);

			await _apiContext.SaveChangesAsync();
		}
	}
}

