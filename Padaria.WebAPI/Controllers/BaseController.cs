using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Padaria.Application.ViewModels;
using Padaria.Domain.Entities.Base;
using Padaria.Domain.Entities.Base.Helpers;
using Padaria.Infra.Data.Repository;
using Padaria.Infra.Data.Repository.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Padaria.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TEntity, TViewModel> : ControllerBase
        where TEntity : EntityBase
        where TViewModel : class
    {
        public readonly IBaseRepository<TEntity> _repo;
        public readonly IMapper _mapper;

        public BaseController(IBaseRepository<TEntity> repo, IMapper mapper)
        {

            _repo = repo;
            _mapper = mapper;
        }


        /// <summary>
        /// Requisição usada para pegar(GET) informações
        /// </summary>
        /// <param name="PagNumero">Numero da página(padrão é 1)</param>
        /// <param name="PagRegistro">Quantidade de Registro por página(padrão é 10)</param>
        /// <returns></returns>

        //GET api/entidade?PagNumero=number&PagRegistro=number
        [HttpGet]
        public async Task<IActionResult> Get([Required] int PagNumero = 1, [Required] int PagRegistro = 10)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var paging = await _repo.GetPagingAsync(PagNumero, PagRegistro);

                return Ok(paging);

                //return Ok(_mapper.Map<Paginacao<TViewModel>>(paging));
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

        /// <summary>
        /// Requisição usada para pegar uma entidade existente pelo ID
        /// </summary>
        /// <param name="id">Passe o GUID aqui</param>
        /// <returns></returns>

        //GET api/entidade/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var entity = await _repo.GetByIdAsync(id);
                if (entity == null) return NotFound();

                var results = _mapper.Map<TEntity>(entity);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

        }

        /// <summary>
        /// Requisição usada para adicionar informações a uma entidade
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>

        //POST api/produto
        [HttpPost]
        public async Task<IActionResult> Post(TViewModel viewModel)
        {
            try
            {
                //Mapeamento inverso do Get, deve estar definido no automappper
                var entity = _mapper.Map<TEntity>(viewModel);

                entity.Id = Guid.NewGuid();

                await _repo.AddAsync(entity);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/[Controller]/{entity.Id}", _mapper.Map<TViewModel>(entity));
                }


            }
            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            return BadRequest();

        }



        /// <summary>
        /// A Requisição adiciona um array de entidades
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>

        //POST api/entity
        [HttpPost("Range")]        
        public async Task<IActionResult> PostRange(TViewModel[] viewModel)
        {
            try
            {
                //Mapeamento inverso do Get, deve estar definido no automappper
                var entities = _mapper.Map<TEntity[]>(viewModel);

                //entity.Id = Guid.NewGuid();

                await _repo.AddRangeAsync(entities);

                if (await _repo.SaveChangesAsync())
                {
                    return Ok(entities);
                    //return Created($"/api/[Controller]/{entity.Id}", _mapper.Map<TViewModel>(entity));
                }


            }
            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            return BadRequest();

        }


        /// <summary>
        /// Requisição usada para excluir uma entidade existente passando o ID dela
        /// </summary>
        /// <param name="id">Passe o GUID aqui</param>
        /// <returns></returns>

        //DELETE api/entidade/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var entity = await _repo.GetByIdAsync(id);
                if (entity == null) return NotFound();

                await _repo.DeleteAsync(entity);

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


        /// <summary>
        /// Requisição usada para excluir várias entidades passando o ID dela
        /// </summary>
        /// <param name="id">Passe o GUID aqui</param>
        /// <returns></returns>

        //DELETE api/entidade/{id}
        [HttpDelete("Range/{id}")]
        public async Task<IActionResult> DeleteRange(Guid id)
        {
            try
            {
                var entity = await _repo.GetByIdAsync(id);
                if (entity == null) return NotFound();

                await _repo.DeleteAsync(entity);

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


        /// <summary>
        /// Requisição usada para modificar uma entidade existente
        /// </summary>
        /// <param name="id">O GUID deve ser passado por aqui</param>
        /// <param name="viewModel">Passe todos os campos com a modificação. Não é necessario colocar o ID novamente </param>
        /// <returns></returns>

        //PUT api/entidade/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, TViewModel viewModel)
        {
            try
            {

                var entity = await _repo.GetByIdAsync(id);
                if (entity == null) return NotFound();


                _mapper.Map(viewModel, entity);

                entity.Id = id;

                await _repo.UpdateAsync(entity);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/produto/{entity.Id}", _mapper.Map<TViewModel>(entity));
                }


            }
            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }

            return BadRequest();

        }


        /// <summary>
        /// Requisição usada para modificar uma entidade existente de forma pacial
        /// </summary>
        /// <param name="id">O GUID deve ser passado por aqui</param>
        /// <param name="viewModel">Passe apenas os campos modificados conforme necessário. Não é necessario colocar o ID novamente</param>
        /// <returns></returns>
        
        //PATCH api/entidade/{id}
        [HttpPatch("{id}")]
        [ReadableBodyStream]
        public async Task<IActionResult> Patch(Guid id, [FromBody] TViewModel viewModel)
        {
            try
            {
                var entity = await _repo.GetByIdAsync(id);
                if (entity == null) return NotFound();

                HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);

                StreamReader streamReader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8);
                JObject obj = JsonConvert.DeserializeObject<JObject>(await streamReader.ReadToEndAsync());

                foreach (JProperty property in obj.Properties())
                {
                    Type type = entity.GetType();
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

                        propertyAux.SetValue(entity, value);
                    }
                }

                entity.Id = id;

                await _repo.UpdateAsync(entity);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/produto/{entity.Id}", _mapper.Map<TViewModel>(entity));
                }
            }
            catch (System.Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou" + ex);
            }

            return BadRequest();

        }
    }
}
