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
    public class ScheduledTaskController : Controller
    {
        private readonly ScheduledTaskService _scheduledTaskService;

        public ScheduledTaskController(ApiContext context, IMapper mapper)
        {
            _scheduledTaskService = new ScheduledTaskService(
                context ?? throw new ArgumentNullException(nameof(context)),
                mapper ?? throw new ArgumentNullException(nameof(mapper)));
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_scheduledTaskService.FindAllScheduledTasks());
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
                return Ok(await _scheduledTaskService.FindScheduledTaskAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/values/5
        [HttpGet]
        [Route("GetScheduledTasksByRoomMate")]
        public IActionResult GetScheduledTasksByRoomMate(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("id is invalid");

            try
            {
                return Ok(_scheduledTaskService.FindAllScheduledTasksByRoomMateInThisWeek(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // GET api/values/5
        [HttpGet]
        [Route("GetScheduledTasksByWeek")]
        public IActionResult GetScheduledTasksByWeek(int week)
        {
            if (week <= 0 && week > 53)
                return BadRequest("week is invalid");

            try
            {
                return Ok(_scheduledTaskService.FindAllScheduledTasksByWeek(week));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Get api/values
        [HttpGet]
        [Route("GetUnassignedWeekSchedule")]
        public IActionResult GetUnassignedWeekSchedule(int week)
        {
            if (week <= 0 && week > 53)
                return BadRequest("week is invalid");

            try
            {
                return Ok(_scheduledTaskService.GetWeekScheduleForPlanning(week));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // Get api/values
        [HttpGet]
        [Route("GetThisWeekSchedule")]
        public IActionResult GetThisWeekSchedule()
        {
            try
            {
                return Ok(_scheduledTaskService.GetThisWeekSchedule());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ScheduledTaskModel model)
        {
            if (model == null)
                return BadRequest("name is invalid");

            try
            {
                await _scheduledTaskService.CreateScheduledTaskServiceAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/values
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ScheduledTaskModel model)
        {
            if (model == null)
                return BadRequest("model is invalid");

            try
            {
                await _scheduledTaskService.UpdateScheduledTaskAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut]
        [Route("PutFinishedTask")]
        public async Task<IActionResult> PutFinishedTask(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("id is invalid");

            try
            {
                await _scheduledTaskService.FinishedScheduledTaskAsync(id);
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
                await _scheduledTaskService.DeleteScheduledTaskAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

