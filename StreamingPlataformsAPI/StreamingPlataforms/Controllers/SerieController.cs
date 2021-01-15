using StreamingPlataforms.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StreamingPlataforms.Models;
using StreamingPlataforms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StreamingPlataforms.Controllers
{
    [Route("api/plataform/{plataformId}/[controller]")]
    //[Route("api/[controller]")]

    public class SerieController : ControllerBase
    {
        private ISerieService service;
        public SerieController(ISerieService service)
        {
            this.service = service;
        }
        // GET: api/<SerieController>
        [HttpGet("{serieId}")]
        public async Task<ActionResult<SerieModel>> GetSerieAsync(int plataformId, int serieId)
        {
            try
            {
                return Ok(await service.GetSerieAsync(plataformId, serieId));
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

        [HttpGet]
        public async Task<ActionResult<SerieModel>> GetSeriesAsync(int plataformId)
        {
            try
            {
                 return Ok(await service.GetSeriesAsync(plataformId));
                //return Ok(await service.getByPlataformIdAsync(plataformId));

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult<SerieModel>> CreateSerieAsync(int plataformId, [FromBody] SerieModel serie)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var newSerie = await service.CreateSerieAsync(plataformId, serie);
                return Created($"api/plataform/{plataformId}/serie/{newSerie.SerieId}", newSerie);

            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            /*catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message); ;
            }*/
        }
            
        [HttpDelete("{serieId}")]
        public async Task<IActionResult> DeleteSerie(int plataformId, int serieId)
        {
            try
            {
                return Ok(await service.DeleteSerieAsync(plataformId, serieId));
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

        [HttpPut("{serieId}")]
        public async Task<ActionResult<bool>> UpdateSerieAsync(int plataformId, int serieId, [FromBody] SerieModel serie)
        {
            try
            {
                return Ok(await service.UpdateSerieAsync(plataformId, serieId, serie));
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
