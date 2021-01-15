using StreamingPlataforms.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamingPlataforms.Contexts;
using StreamingPlataforms.Data.Entities;
using StreamingPlataforms.Models;
using StreamingPlataforms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StreamingPlataforms.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class PlataformController : ControllerBase
    {
        private IPlataformService service;
        public PlataformController(IPlataformService service)
        {
            this.service = service;
        }
        // GET: api/<PlataformController>
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<PlataformModel>>> GetPlataforms(string orderBy = "id", bool showSeries = true)
        {
            var user = User;

            try
            {
                return Ok(await service.GetPlataformsAsync(orderBy, showSeries));
            }
            catch (BadOperationRequest ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlataformModel>> GetPlataformAsync(int id)
        {
            try
            {
                return Ok(await service.GetPlataformAsync(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PlataformModel>> CreatePlataform([FromBody] PlataformModel plataform)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newPlataform = await service.CreatePlataformAsync(plataform);
                return Created($"/api/plataform/{newPlataform.Id}", newPlataform);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); ;
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeletePlatformAsync(int id)
        {
            try
            {
                var dlr = await service.DeletePlataformAsync(id);
                return Ok(dlr);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); ;
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdatePlataform(int id, [FromBody] PlataformModel plataform)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    foreach (var state in ModelState)
                    {
                        if (state.Key == nameof(plataform.FundationYear) && state.Value.Errors.Count > 0)
                        {
                            return BadRequest(state.Value.Errors);
                        }
                    }
                }

                return Ok(await service.UpdatePlataformAsync(id, plataform));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
