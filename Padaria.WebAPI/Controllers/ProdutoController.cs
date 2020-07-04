using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Padaria.Domain;
using Padaria.Repository;
using Padaria.WebAPI.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;


namespace Padaria.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]//Já identifica as validações e as requisiçoes via corpo
    public class ProdutoController : ControllerBase
    {
        public readonly IPadariaRepository _repo;
        private readonly IMapper _mapper;

        public ProdutoController(IPadariaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        //GET api/produto
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var produtos = await _repo.GetAllProdutosAsync();

                var results = _mapper.Map<ProdutoDto[]>(produtos);
                //Precisa do IEnumerable porque retorna mais de um produto

                return Ok(results);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

        }

        //Upload da Imagem
        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, filename.Replace("\"", " ").Trim());

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                return Ok();
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            //return BadRequest("Erro ao tentar realizar upload");

        }

        //GET api/produto/id
        [HttpGet("{ProdutoId}")]
        public async Task<IActionResult> Get(Guid ProdutoId)
        {
            try
            {
                var produtos = await _repo.GetAllProdutosAsyncById(ProdutoId);

                var results = _mapper.Map<ProdutoDto>(produtos);

                return Ok(results);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

        }

        //POST api/produto
        [HttpPost]
        public async Task<IActionResult> Post(ProdutoDto model)//FromBody
        {
            try
            {
                //Mapeamento inverso do Get, deve estar definido no automappper
                var produto = _mapper.Map<Produto>(model);

                _repo.Add(produto);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/produto/{model.Id}", _mapper.Map<ProdutoDto>(produto));
                }

                
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            return BadRequest();

        }


         //PUT api/produto
        [HttpPut("{ProdutoId}")]
        public async Task<IActionResult> Put(Guid ProdutoId, ProdutoDto model)
        {
            try
            {

                var produto = await _repo.GetAllProdutosAsyncById(ProdutoId);
                if (produto == null) return NotFound();

                _mapper.Map(model, produto);

                _repo.Update(produto);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/produto/{model.Id}", _mapper.Map<ProdutoDto>(produto));
                }


            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            return BadRequest();

        }


        //DELETE api/WeatherForecast
        [HttpDelete("{ProdutoId}")]
        public async Task<IActionResult> Delete(Guid ProdutoId)
        {
            try
            {
                var produto = await _repo.GetAllProdutosAsyncById(ProdutoId);
                if (produto == null) return NotFound();

                _repo.Delete(produto);

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
