using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NET_demo.DatabaseContext;
using ASP.NET_demo.Models;
using ASP.NET_demo.Services;
using ASP.NET_demo.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP.NET_demo.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly TaskService _taskService;

        public TaskController(ApiContext context, IMapper mapper)
        {
            _taskService = new TaskService(
                context ?? throw new ArgumentNullException(nameof(context)),
                mapper ?? throw new ArgumentNullException(nameof(mapper)));
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_taskService.FindAllTasks());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("id is invalid");

            try
            {
                return Ok(await _taskService.FindTaskAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TaskModel model)
        {
            if (model == null)
                return BadRequest("name is invalid");

            try
            {
                await _taskService.CreateTaskAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/values
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TaskViewModel model)
        {
            if (model == null)
                return BadRequest("model is invalid");

            try
            {
                await _taskService.UpdateTaskAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("id is invalid");

            try
            {
                await _taskService.DeleteTaskAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

