using DataAccess.Models;
using Infraestructura.Contratos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api_Log.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
    
        private readonly IRepositoryAsync<Categoria> _repositoryAsync;

        public CategoriaController(
         
            IRepositoryAsync<Categoria> repositoryAsync
            )
        {
            
            _repositoryAsync = repositoryAsync;

        }

        [HttpGet]
        public async Task<IEnumerable<Categoria>> Listar()
        {
            var rt = await _repositoryAsync.GetAll();

           // var rt = await _modelContext.Catego.OrderBy(rt => rt.Id).ToListAsync();
            return rt;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> BuscarPorId(decimal? id)
        {

            var retorno = await _repositoryAsync.GetByID(id);
            if (retorno != null)
                return retorno;
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> Guardar(Categoria c)
        {
            try
            {
                var rt = await _repositoryAsync.Insert(c);
      
                return rt;
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Se encontró un error");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Categoria>> Actualizar(Categoria c)
        {
            if (c == null || c.Id == 0)
                return BadRequest("Faltan datos");

            Categoria cat = await _repositoryAsync.GetByID(c.Id); 

            if (cat == null)
                return NotFound();

            try
            {
                cat.Nombre = c.Nombre;
                await _repositoryAsync.Update(cat);
                return cat;
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Se encontró un error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> Eliminar(decimal id)
        {
            Categoria cat = await _repositoryAsync.GetByID(id);  
           

            if (cat == null)
                return NotFound();

            try
            {
           
               var rt = await _repositoryAsync.Delete(id);
               // await _modelContext.SaveChangesAsync();
                return rt;
            }
            catch (DbUpdateException)
            {
                return StatusCode(500, "Se encontró un error");
            }
        }
    }
}
