using examenEFCrudMVC_API.Context;
using examenEFCrudMVC_API.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace examenEFCrudMVC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly examenEFCrudMVC_API_NOSPContext _context;
        public DepartamentoController(examenEFCrudMVC_API_NOSPContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ListaDepartamentos")]
        public async Task<ActionResult<List<DepartamentoDTO>>> Listar()
        {
            try
            {
                var _listaDto = new List<DepartamentoDTO>();
                var _listaBD = await _context.Departamentos.ToListAsync();
                foreach (var deps in _listaBD)
                {
                    _listaDto.Add(new DepartamentoDTO
                    {
                        IdDepartamento = deps.IdDepartamento,
                        NombreDepartamento = deps.NombreDepartamento
                    });
                }
                return Ok(_listaDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
