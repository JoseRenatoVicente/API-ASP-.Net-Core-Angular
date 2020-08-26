using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Padaria.Application.ViewModels;
using Padaria.Domain.Entities;
using Padaria.Domain.Entities.Base.Helpers;
using Padaria.Infra.Data.Context;
using Padaria.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Padaria.WebAPI.Controllers
{

    public class ReadableBodyStreamAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.HttpContext.Request.EnableBuffering();
        }
    }



    [Route("api/[controller]")]
    [ApiController]//Já identifica as validações e as requisiçoes via corpo
    public class Produto1Controller : ControllerBase
    {
        public readonly IPadariaRepository _repo;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public Produto1Controller(IPadariaRepository repo, IMapper mapper, DataContext context)
        {

            _repo = repo;
            _mapper = mapper;
            _context = context;
        }
        

        /// <summary>
        /// Usado para um admin ver os produtos incluindo os indisponiveis (necessário autenticação)
        /// </summary>
        /// <param name="PagNumero">Numero da página(padrão é 1)</param>
        /// <param name="PagRegistro">Quantidade de Registro por página(padrão é 4)</param>
        /// <returns></returns>

        //GET api/produto/Get-Admin
        [HttpGet("Get-Admin")]
        public async Task<IActionResult> GetAdmin(int? PagNumero = 1, int? PagRegistro = 4)
        {
            try
            {
                var produtos = await _repo.GetAllProdutosAsyncAdmin();

                var results = _mapper.Map<ProdutoViewModel[]>(produtos).AsQueryable();


                if (PagNumero.HasValue & PagRegistro.HasValue)
                {
                    var quantidadeTotalRegistros = results.Count();
                    results = results.Skip((PagNumero.Value - 1) * PagRegistro.Value).Take(PagRegistro.Value);

                    var paginacao = new Paginacao<ProdutoViewModel>();
                    paginacao.NumeroPagina = PagNumero.Value;
                    paginacao.RegistroPorPagina = PagRegistro.Value;
                    paginacao.TotalRegistros = quantidadeTotalRegistros;
                    paginacao.TotalPaginas = (int)Math.Ceiling((double)quantidadeTotalRegistros / PagRegistro.Value); // quantidadeTotalRegistros / pagResgistro = TotalPaginas(arredondar)

                    Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginacao));

                    if (PagNumero > paginacao.TotalPaginas)
                    {
                        return NotFound();
                    }

                }

                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        /// <summary>
        /// Pega um Produto informando o ID dele que um GUID (necessário autenticação)
        /// </summary>
        /// <param name="ProdutoId">O GUID deve ser passado por aqui</param>
        /// <returns></returns>

        //GET api/produto/{ProdutoId}
        [HttpGet("{ProdutoId}")]
        public async Task<IActionResult> Get(Guid ProdutoId)
        {
            try
            {
                var produto = await _repo.GetAllProdutosAsyncById(ProdutoId);
                if (produto == null) return NotFound();

                var results = _mapper.Map<ProdutoViewModel>(produto);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

        }

        /// <summary>
        /// Faz o upload de uma Imagem (necessário autenticação)
        /// </summary>
        /// <returns></returns>

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
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar realizar upload");
            }

        }

        /// <summary>
        /// Metodo usado para o cadastro de produtos (necessário autenticação)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        //POST api/produto
        [HttpPost]
        public async Task<IActionResult> Post(ProdutoViewModel model)
        {
            try
            {
                model.Id = Guid.NewGuid();

                //Mapeamento inverso do Get, deve estar definido no automappper
                var produto = _mapper.Map<Produto>(model);

                await _repo.AddAsync(produto);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/produto/{model.Id}", _mapper.Map<ProdutoViewModel>(produto));
                }


            }
            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            return BadRequest();

        }



        /// <summary>
        /// Metodo usado para a edição de um produto existe, não é necessario passar o ID apenas no cabeçalho
        /// </summary>
        /// <param name="ProdutoId">O GUID deve ser passado por aqui</param>
        /// <param name="model"></param>
        /// <returns></returns>

        //PUT api/produto
        [HttpPut("{ProdutoId}")]
        public async Task<IActionResult> Put(Guid ProdutoId, ProdutoViewModel model)
        {
            try
            {

                var produto = await _repo.GetAllProdutosAsyncById(ProdutoId);
                if (produto == null) return NotFound();

                model.Id = ProdutoId;

                _mapper.Map(model, produto);

                _repo.Update(produto);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/produto/{model.Id}", _mapper.Map<ProdutoViewModel>(produto));
                }


            }
            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            return BadRequest();

        }


        /// <summary>
        /// Metodo usado para a edição de um produto existe, não é necessario passar o ID apenas no cabeçalho
        /// </summary>
        /// <param name="ProdutoId">O GUID deve ser passado por aqui</param>
        /// <returns></returns>

        [ReadableBodyStream]
        [AllowAnonymous]
        //PUT api/produto
        [HttpPatch("{ProdutoId}")]
        public async Task<IActionResult> Patch(Guid ProdutoId)
        {
            try
            {
                var produto = await _repo.GetAllProdutosAsyncById(ProdutoId);
                if (produto == null) return NotFound();

                StreamReader streamReader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8);
                JObject obj = JsonConvert.DeserializeObject<JObject>(await streamReader.ReadToEndAsync());

                foreach (JProperty property in obj.Properties())
                {
                    Type type = produto.GetType();
                    var propertyAux = type.GetProperties().FirstOrDefault(x => x.Name.ToLower() == property.Name.ToLower());
                    if (propertyAux != null)
                    {
                        object value = null;

                        if (propertyAux.PropertyType == typeof(string))
                        {
                            value = property.Value.ToString();
                        }
                        else if (propertyAux.PropertyType == typeof(int) && int.TryParse(property.Value.ToString(), out int integer))
                        {
                            value = integer;
                        }
                        else if (propertyAux.PropertyType == typeof(double) && double.TryParse(property.Value.ToString(), out double doubleNumber))
                        {
                            value = doubleNumber;
                        }

                        propertyAux.SetValue(produto, value);
                    }
                }

                _repo.Update(produto);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/produto/{produto.Id}", _mapper.Map<ProdutoViewModel>(produto));
                }
            }
            catch (System.Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou" + ex);
            }

            return BadRequest();

        }


        /// <summary>
        /// Metodo usado para excluir um produto passando o ID dele
        /// </summary>
        /// <param name="ProdutoId">Passe o GUID aqui</param>
        /// <returns></returns>

        //DELETE api/produto/{ProdutoId}
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

                return StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            return BadRequest();

        }

    }
}
