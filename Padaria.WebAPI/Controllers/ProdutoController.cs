using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Padaria.Application.ViewModels;
using Padaria.Domain.Entities;
using Padaria.Infra.Data.Repository;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Padaria.WebAPI.Controllers
{
    public class ProdutoController : BaseController<Produto, ProdutoViewModel>
    {
        public ProdutoController(IProdutoRepository repo, IMapper mapper) : base(repo, mapper)
        {
        }


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

        


        [HttpGet("Get-User")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser([Required] int PagNumero = 1, [Required] int PagRegistro = 4)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                //var get =  _repo.ListarPor(x => x.Status == "1"); 

                
                var paging = await _repo.GetPagingAsyncWhere(c => c.Status == "1", PagNumero, PagRegistro);

                //var get = await _repo.ListarPor(c => c.Status == "1");


                return Ok(paging);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }

    }
}
