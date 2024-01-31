using MaxiTransfers.Api.Entidades.DTO.Empleado;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaxiTransfers.Api.Servicios
{
    public interface IEmpleadosService
    {
        public Task<EmpleadosResponse> CrearEmpleado(EmpleadosRequestCreate prestamos);
        public Task<List<EmpleadosResponse>> ListarEmpleados();
        public Task<EmpleadosResponse> ObtenerEmpleado(int NumEmpleado);
        public Task<bool> ActualizarEmpleado(EmpleadosRequestUpdate NumEmpleado);
        public Task<bool> EliminarEmpleado(int NumEmpleado);
    }
}
