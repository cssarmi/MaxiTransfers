using MaxiTransfers.Api.Entidades.DTO.Empleado;
using MaxiTransfers.Api.Entidades.SoporteDTO;
using MaxiTransfers.Api.Modelo;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace MaxiTransfers.Api.Servicios
{
    public class EmpleadosService : IEmpleadosService
    {
        private readonly PersistenceContext _objcone;

        public EmpleadosService(PersistenceContext objcone)
        {
            _objcone = objcone;
        }

        public async Task<EmpleadosResponse> CrearEmpleado(EmpleadosRequestCreate obj)
        {
            Empleados objempleado = new Empleados()
            {
                Nombre = obj.Nombre,
                Apellidos = obj.Apellidos,
                FechaNacimiento = obj.FechaNacimiento,
                CURP = obj.CURP,
                SSN = obj.SSN,
                telefono = obj.Telefono,
                Nacionalidad = obj.Nacionalidad

            };
            _objcone.Empleados.Add(objempleado);
            await _objcone.SaveChangesAsync();

            var res = await _objcone.Empleados.OrderByDescending(x => x.NumEmpleado).FirstOrDefaultAsync();
            EmpleadosResponse objrespuesta = new EmpleadosResponse()
            {
                NumEmpleado = res.NumEmpleado,
                Nombre = res.Nombre,
                Apellidos = res.Apellidos,
                FechaNacimiento = res.FechaNacimiento,
                CURP = res.CURP,
                SSN = res.SSN,
                Telefono = res.telefono,
                Nacionalidad = res.Nacionalidad
            };
            return objrespuesta;
        }

        public async Task<List<EmpleadosResponse>> ListarEmpleados()
        {
            List<EmpleadosResponse> objres = new List<EmpleadosResponse>();
            var res = await _objcone.Set<Empleados>().FromSqlRaw("EXEC SP_ListarEmpleados").ToListAsync();

            if (res != null)
            {
                foreach (var obj in res)
                {
                    EmpleadosResponse empleadosResponseCreate = new EmpleadosResponse()
                    {
                        NumEmpleado = obj.NumEmpleado,
                        Nombre = obj.Nombre,
                        Apellidos = obj.Apellidos,
                        FechaNacimiento = obj.FechaNacimiento,
                        CURP = obj.CURP,
                        SSN = obj.SSN,
                        Telefono = obj.telefono,
                        Nacionalidad = obj.Nacionalidad
                    };
                    objres.Add(empleadosResponseCreate);
                }
            }
            return objres;
        }

        public async Task<EmpleadosResponse> ObtenerEmpleado(int objempleado)
        {
            var parameter1Param = new SqlParameter("@Numempleado", SqlDbType.Int)
            {
                Value = objempleado
            };
            var Objempleado = await _objcone.Set<Empleados>().FromSqlRaw("EXEC SP_ListarEmpleadosXId @Numempleado", parameter1Param).ToListAsync();
            EmpleadosResponse Objres = new EmpleadosResponse();
            if (Objempleado.Count > 0)
            {
                
                Objres.NumEmpleado = Objempleado[0].NumEmpleado;
                Objres.Nombre = Objempleado[0].Nombre;
                Objres.Apellidos = Objempleado[0].Apellidos;
                Objres.FechaNacimiento = Objempleado[0].FechaNacimiento;
                Objres.CURP = Objempleado[0].CURP;
                Objres.SSN = Objempleado[0].SSN;
                Objres.Telefono = Objempleado[0].telefono;
                Objres.Nacionalidad = Objempleado[0].Nacionalidad;
            }
            return Objres;
        }

        public async Task<bool> ActualizarEmpleado(EmpleadosRequestUpdate obj)
        {

            var existeEmpleado = _objcone.Empleados.AsNoTracking().FirstOrDefault(x => x.NumEmpleado == obj.NumEmpleado);

            if (existeEmpleado != null)
            {
                Empleados objprestamo = new Empleados()
                {
                    NumEmpleado = obj.NumEmpleado,
                    Nombre = obj.Nombre,
                    Apellidos = obj.Apellidos,
                    FechaNacimiento = obj.FechaNacimiento,
                    CURP = obj.CURP,
                    SSN = obj.SSN,
                    telefono = obj.Telefono,
                    Nacionalidad = obj.Nacionalidad
                };
                _objcone.Empleados.Update(objprestamo);
                await _objcone.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }

        public async Task<bool> EliminarEmpleado(int Empleado)
        {

            var res = await _objcone.Empleados.FindAsync(Empleado);

            if (res != null)
            {
                _objcone.Remove(res);
                await _objcone.SaveChangesAsync();
                return true;
            }
            else { return false; }

        }
    }
}
