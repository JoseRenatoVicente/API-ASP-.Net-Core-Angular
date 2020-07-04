using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Padaria.Repository;

namespace Padaria.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public DataContext _context { get; }//ja esta como readonly
        //ou
        //public readonly DataContext _context;
        
        public WeatherForecastController(DataContext context)
        {
            _context = context;

        }

        //GET api/WeatherForecast
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results= await _context.Produtos.ToListAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
            
        }

        //GET api/WeatherForecast/1
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            try
            {
                var results = await _context.Produtos.FirstOrDefaultAsync(x=> x.Id == id);
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");                
            }

            
        }
    }
}
