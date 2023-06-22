using System;
using System.Globalization;
using ASP.NET_demo.DatabaseContext;
using ASP.NET_demo.Models;
using ASP.NET_demo.ViewModels;
using AutoMapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ASP.NET_demo.Services
{
	public class ScheduledTaskService
	{
        private readonly ApiContext _apiContext;
        private readonly IMapper _mapper;

        public ScheduledTaskService(ApiContext apiContext, IMapper mapper)
        {
            _apiContext = apiContext
                ?? throw new ArgumentNullException(nameof(apiContext));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task CreateScheduledTaskServiceAsync(ScheduledTaskModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            await _apiContext.ScheduledTasks.AddAsync(model);

            await _apiContext.SaveChangesAsync();
        }

        public async Task<ScheduledTaskViewModel> FindScheduledTaskAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            var roomMate = await _apiContext.ScheduledTasks.FindAsync(id)
                ?? throw new KeyNotFoundException(nameof(id));

            return _mapper.Map<ScheduledTaskViewModel>(roomMate);
        }

        public List<ScheduledTaskViewModel> FindAllScheduledTasksByWeek(int week)
        {
            if (week <= 0)
                throw new ArgumentOutOfRangeException(nameof(week));

            return _apiContext
                .ScheduledTasks
                .Where(x => x.Week == week)
                .Select(x => _mapper.Map<ScheduledTaskViewModel>(x))
                .ToList();
        }

        public List<ScheduledTaskViewModel> FindAllScheduledTasksByRoomMateInThisWeek(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;
            var actualWeek = cal
                .GetWeekOfYear(
                    DateTime.Now,
                    dfi.CalendarWeekRule,
                    dfi.FirstDayOfWeek
            );

            return _apiContext
                .ScheduledTasks
                .Where(x => x.Week == actualWeek && x.RoomMateId == id)
                .Select(x => _mapper.Map<ScheduledTaskViewModel>(x))
                .ToList();
        }

        public List<ScheduledTaskViewModel> FindAllScheduledTasks()
        {
            return _apiContext
                .ScheduledTasks
                .Select(x => _mapper.Map<ScheduledTaskViewModel>(x))
                .ToList();
        }

        public async Task UpdateScheduledTaskAsync(ScheduledTaskModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var scheduledTask = await _apiContext.ScheduledTasks.FindAsync(model.Id)
                ?? throw new KeyNotFoundException(nameof(model));
            _apiContext.ScheduledTasks.Update(model);

            await _apiContext.SaveChangesAsync();
        }

        public async Task DeleteScheduledTaskAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            var scheduledTask = await _apiContext.ScheduledTasks.FindAsync(id)
                ?? throw new KeyNotFoundException(nameof(id));

            _apiContext.ScheduledTasks.Remove(scheduledTask);

            await _apiContext.SaveChangesAsync();
        }
    }
}

