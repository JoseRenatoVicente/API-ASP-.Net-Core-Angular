using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Padaria.Domain;
using Padaria.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace Padaria.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PedidoController : ControllerBase
    {
        public readonly IPadariaRepository _repo;

        public PedidoController(IPadariaRepository repo)
        {
            _repo = repo;
        }

        //GET api/WeatherForecast
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repo.GetAllPedidosAsync();

                return Ok(results);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

        }


        //GET api/WeatherForecast
        [HttpGet("{PedidoId}")]
        public async Task<IActionResult> Get(Guid PedidoId)
        {
            try
            {
                var results = await _repo.GetAllPedidosAsyncById(PedidoId);

                return Ok(results);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

        }

        //POST api/WeatherForecast
        [HttpPost]
        public async Task<IActionResult> Post(Pedido model)
        {
            try
            {
                _repo.Add(model);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/pedido/{model.Id}", model);
                }

                
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            return BadRequest();

        }


        //PUT api/WeatherForecast
        [HttpPut]
        public async Task<IActionResult> Put(Guid PedidoId, Pedido model)
        {
            try
            {

                var pedido = await _repo.GetAllPedidosAsyncById(PedidoId);
                if (pedido == null) return NotFound();

                _repo.Update(model);


                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/pedido/{model.Id}", model);
                }


            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            return BadRequest();

        }


        //DELETE api/WeatherForecast
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid PedidoId)
        {
            try
            {
                var pedido = await _repo.GetAllPedidosAsyncById(PedidoId);
                if (pedido == null) return NotFound();

                _repo.Delete(pedido);

                if (await _repo.SaveChangesAsync())
                {
                    return Ok();
                }


            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            return BadRequest();

        }

    }
}
