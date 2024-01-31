using MaxiTransfers.Api.Entidades.DTO.Beneficiario;
using MaxiTransfers.Api.Entidades.DTO.Empleado;
using MaxiTransfers.Api.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace MaxiTransfers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosBeneficiariosController : ControllerBase
    {
        private readonly IEmpleadosService _empleadoService;
        private readonly IBeneficiariosService _beneficiarioService;
        public EmpleadosBeneficiariosController(IEmpleadosService prestamoService,
            IBeneficiariosService beneficiariosService)
        {
            _empleadoService = prestamoService;
            _beneficiarioService = beneficiariosService;
        }

        #region Empleados
        /// <summary>
        /// Endpoint encargado de Crear empleados en la base de datos
        /// </summary>
        /// /// <param name="Objempleado">Objeto que tiene todas las propiedades de un empleado </param>
        /// <returns></returns>
        [HttpPost]
        [Route("PostEmpleados")]
        public async Task<IActionResult> PostEmpleados(EmpleadosRequestCreate Objempleado)
        {
            var response = _empleadoService.CrearEmpleado(Objempleado);
            return Ok(response.Result);
        }

        /// <summary>
        /// Endpoint encargado de traer los empleados de la base de datos  por id 
        /// </summary>
        /// /// <param name="idempleado">Numero de identificacion del empleado</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEmpleados")]
        public async Task<IActionResult> GetEmpleados(int idempleado)
        {
            var res = await _empleadoService.ObtenerEmpleado(idempleado);
            return Ok(res);
        }

        /// <summary>
        /// Endpoint encargado de traer los empleados de la base de datos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("listarEmpleados")]
        public async Task<IActionResult> listarEmpleados()
        {
            var res = await _empleadoService.ListarEmpleados();
            return Ok(res);
        }

        /// <summary>
        /// Endpoint encargado de actualizar los empleados de la base de datos
        /// </summary>
        /// <param name="Objempleado"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("PutEmpleados")]
        public async Task<IActionResult> PutEmpleados(EmpleadosRequestUpdate Objempleado)
        {
            var response = _empleadoService.ActualizarEmpleado(Objempleado);
            return Ok(response.Result);
        }

        /// <summary>
        /// Endpoint encargado de eliminar los empleados de la base de datos por id
        /// </summary>
        /// <param name="empleado"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteEmpleados")]
        public async Task<IActionResult> DeleteEmpleados(int empleado)
        {
            var response = _empleadoService.EliminarEmpleado(empleado);
            return Ok(response.Result);
        }
        #endregion

        #region Beneficiarios
        /// <summary>
        /// Endpoint encargado de Crear Beneficiarios en la base de datos
        /// </summary>
        /// /// <param name="Objbeneficiario">Objeto que tiene todas las propiedades de un beneficiario </param>
        /// <returns></returns>
        [HttpPost]
        [Route("PostBeneficiarios")]
        public async Task<IActionResult> PostBeneficiarios(BeneficiariosRequestCreate Objbeneficiario)
        {
            var response = await _beneficiarioService.CrearBeneficiario(Objbeneficiario);
            if (response != null)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(404, $"El valor del porcentaje de participacion es mayor al permitido");
            }


        }

        /// <summary>
        /// Endpoint encargado de traer los Beneficiarios de la base de datos  por id 
        /// </summary>
        /// /// <param name="idbeneficiario">Numero de identificacion del beneficiario</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBeneficiarios")]
        public async Task<IActionResult> GetBeneficiarios(int idbeneficiario)
        {
            var res = await _beneficiarioService.ObtenerBeneficiarios(idbeneficiario);
            return Ok(res);
        }

        /// <summary>
        /// Endpoint encargado de traer los Beneficiarios de la base de datos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("listarBeneficiarios")]
        public async Task<IActionResult> listarBeneficiarios()
        {
            var res = await _beneficiarioService.ListarBeneficiarios();
            return Ok(res);
        }

        /// <summary>
        /// Endpoint encargado de actualizar los Beneficiarios de la base de datos
        /// </summary>
        /// <param name="Objbeneficiario"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("PutBeneficiarios")]
        public async Task<IActionResult> PutBeneficiarios(BeneficiariosRequestUpdate Objbeneficiario)
        {
            var response = _beneficiarioService.ActualizarBeneficiario(Objbeneficiario);
            return Ok(response.Result);
        }

        /// <summary>
        /// Endpoint encargado de eliminar los Beneficiarios de la base de datos por id
        /// </summary>
        /// <param name="beneficiario"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteBeneficiarios")]
        public async Task<IActionResult> DeleteBeneficiarios(int beneficiario)
        {
            var response = _beneficiarioService.EliminarBeneficiario(beneficiario);
            return Ok(response.Result);
        }
        #endregion
    }
}
