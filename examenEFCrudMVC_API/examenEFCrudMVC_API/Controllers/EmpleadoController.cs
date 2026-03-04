using examenEFCrudMVC_API.Context;
using examenEFCrudMVC_API.DTOS;
using examenEFCrudMVC_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace examenEFCrudMVC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly examenEFCrudMVC_API_NOSPContext _context;
        public EmpleadoController(examenEFCrudMVC_API_NOSPContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ListaEmpleado")]
        public async Task<ActionResult<List<EmpleadoDTO>>> Listar()
        {
            try
            {
                var _listaDto = new List<EmpleadoDTO>();
                var _listaBd = await _context.Empleados.Include(tD => tD.IdDepartamentoNavigation).ToListAsync();
                foreach (var emp in _listaBd)
                {
                    _listaDto.Add(new EmpleadoDTO
                    {
                        IdEmpleado = emp.IdEmpleado,
                        NombreEmpleado = emp.NombreEmpleado,
                        IdDepartamento = emp.IdDepartamento,
                        NombreDepartamento = emp.IdDepartamentoNavigation.NombreDepartamento,
                        Activo = emp.Activo
                    });
                }
                return Ok(_listaDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ObtenerEmpleado/{idEmp}")]
        public async Task<ActionResult<EmpleadoDTO>> Obtener(int idEmp)
        {
            try
            {
                var _empleadoDTO = new EmpleadoDTO();
                var _empleadoBD = await _context.Empleados.
                    Include(tD => tD.IdDepartamentoNavigation).
                    Where(e => e.IdEmpleado == idEmp).FirstAsync();

                _empleadoDTO.IdEmpleado = idEmp;
                _empleadoDTO.NombreEmpleado = _empleadoBD.NombreEmpleado;
                _empleadoDTO.IdDepartamento = _empleadoBD.IdDepartamento;
                _empleadoDTO.NombreDepartamento = _empleadoBD.IdDepartamentoNavigation.NombreDepartamento;
                _empleadoDTO.Activo = _empleadoBD.Activo;

                return Ok(_empleadoDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("GuardarEmpleado")]
        public async Task<ActionResult<EmpleadoDTO>> Guardar(EmpleadoDTO empdto)
        {
            try
            {
                var _empleadoDB = new Empleado
                {
                    NombreEmpleado = empdto.NombreEmpleado,
                    IdDepartamento = empdto.IdDepartamento
                };

                await _context.Empleados.AddAsync(_empleadoDB);
                await _context.SaveChangesAsync();
                return Ok("Empleado Guardado!!!!!!!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("EditarEmpleado")]
        public async Task<ActionResult<EmpleadoDTO>> Editar(EmpleadoDTO empdto)
        {
            try
            {
                var _empleadoBD = await _context.Empleados.
                Where(e => e.IdEmpleado == empdto.IdEmpleado).FirstOrDefaultAsync();

                if (_empleadoBD != null)
                {
                    _empleadoBD.NombreEmpleado = empdto.NombreEmpleado;
                    _empleadoBD.IdDepartamento = empdto.IdDepartamento;
                    _empleadoBD.Activo = empdto.Activo;

                    _context.Empleados.Update(_empleadoBD);
                    await _context.SaveChangesAsync();

                    return Ok(_empleadoBD);
                }
                else
                {
                    return BadRequest($"El usuario con el id {empdto.IdEmpleado} no existe");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("Eliminar/{idEmp}")]
        public async Task<ActionResult> Eliminar(int idEmp)
        {
            try
            {
                var _empleadoBD = await _context.Empleados.
                Where(e => e.IdEmpleado == idEmp).FirstOrDefaultAsync();

                if (_empleadoBD is null)
                {
                    return NotFound("El usuario no existe");
                }
                else
                {
                    _context.Empleados.Remove(_empleadoBD);
                    await _context.SaveChangesAsync();
                    return Ok("Empleado Eliminado");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
