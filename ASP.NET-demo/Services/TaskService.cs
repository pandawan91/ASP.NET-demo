using System;
using ASP.NET_demo.DatabaseContext;
using ASP.NET_demo.Models;
using ASP.NET_demo.ViewModels;
using AutoMapper;

namespace ASP.NET_demo.Services
{
	public class TaskService
	{
        private readonly ApiContext _apiContext;
        private readonly IMapper _mapper;

        public TaskService(ApiContext apiContext, IMapper mapper)
        {
            _apiContext = apiContext
                ?? throw new ArgumentNullException(nameof(apiContext));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task CreateTaskAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            await _apiContext.Tasks.AddAsync(new TaskModel()
            {
                Name = name
            });

            await _apiContext.SaveChangesAsync();
        }

        public async Task<TaskViewModel> FindTaskAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            var task = await _apiContext.Tasks.FindAsync(id)
                ?? throw new KeyNotFoundException(nameof(id));

            return _mapper.Map<TaskViewModel>(task);
        }

        public List<TaskViewModel> FindAllTasks()
        {
            return _apiContext
                .Tasks
                .Select(x => _mapper.Map<TaskViewModel>(x))
                .ToList();
        }

        public async Task UpdateTaskAsync(TaskViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var task = await _apiContext.Tasks.FindAsync(model.Id)
                ?? throw new KeyNotFoundException(nameof(model));
            _apiContext.Tasks.Update(_mapper.Map<TaskModel>(model));

            await _apiContext.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            var task = await _apiContext.Tasks.FindAsync(id)
                ?? throw new KeyNotFoundException(nameof(id));

            _apiContext.Tasks.Remove(task);

            await _apiContext.SaveChangesAsync();
        }
    }
}

