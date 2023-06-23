using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using ASP.NET_demo.DatabaseContext;
using ASP.NET_demo.Services;
using ASP.NET_demo.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP.NET_demo.Controllers
{
    [Route("api/[controller]")]
    public class RoomMateController : Controller
    {
        private readonly RoomMateService _roomMateService;

        public RoomMateController(ApiContext context, IMapper mapper)
        {
            _roomMateService = new RoomMateService(
                context ?? throw new ArgumentNullException(nameof(context)),
                mapper ?? throw new ArgumentNullException(nameof(mapper)));
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_roomMateService.FindAllRoomMates());
            }
            catch(Exception ex)
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
                return Ok(await _roomMateService.FindRoomMateAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("name is invalid");

            try
            {
                await _roomMateService.CreateRoomMateAsync(name);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]RoomMateViewModel model)
        {
            if (model == null)
                return BadRequest("model is invalid");

            try
            {
                await _roomMateService.UpdateRoomMateAsync(model);
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
                await _roomMateService.DeleteRoomMateAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

