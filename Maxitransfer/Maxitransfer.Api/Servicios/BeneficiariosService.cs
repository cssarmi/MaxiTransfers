using MaxiTransfers.Api.Entidades.DTO.Beneficiario;
using MaxiTransfers.Api.Modelo;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MaxiTransfers.Api.Servicios
{
    public class BeneficiariosService : IBeneficiariosService
    {
        private readonly PersistenceContext _objcone;
        public BeneficiariosService(PersistenceContext objcone)
        {
            _objcone = objcone;
        }
        public async Task<string> CrearBeneficiario(BeneficiariosRequestCreate obj)
        {
            var ExisteEmpleado = await _objcone.Empleados.Where(x => x.NumEmpleado == obj.NumEmpleado).FirstOrDefaultAsync();
            var Listporcentaje = await _objcone.Beneficiarios.Where(x => x.NumEmpleado == obj.NumEmpleado).Select(x => x.PorcentajeParticipacion).SumAsync();
            if (ExisteEmpleado != null)
            {
                if (Listporcentaje < 100)
                {
                    var porcentajepermitido = (obj.PorcentajeParticipacion + Listporcentaje <= 100) ? obj.PorcentajeParticipacion : (100 - Listporcentaje);
                    Beneficiarios objbeneficiario = new Beneficiarios()
                    {
                        NumEmpleado = obj.NumEmpleado,
                        Nombre = obj.Nombre,
                        Apellidos = obj.Apellidos,
                        FechaNacimiento = obj.FechaNacimiento,
                        CURP = obj.CURP,
                        SSN = obj.SSN,
                        telefono = obj.Telefono,
                        Nacionalidad = obj.Nacionalidad,
                        PorcentajeParticipacion = porcentajepermitido

                    };
                    _objcone.Beneficiarios.Add(objbeneficiario);
                    await _objcone.SaveChangesAsync();

                    var res = await _objcone.Beneficiarios.OrderByDescending(x => x.NumBeneficiario).FirstOrDefaultAsync();
                    BeneficiariosResponse objrespuesta = new BeneficiariosResponse()
                    {
                        NumBeneficiario = res.NumBeneficiario,
                        NumEmpleado = res.NumEmpleado,
                        Nombre = res.Nombre,
                        Apellidos = res.Apellidos,
                        FechaNacimiento = res.FechaNacimiento,
                        CURP = res.CURP,
                        SSN = res.SSN,
                        Telefono = res.telefono,
                        Nacionalidad = res.Nacionalidad,
                        PorcentajeParticipacion = res.PorcentajeParticipacion
                    };
                    return "Beneficiario Creado con exito";
                }
                else
                {
                    return "El porcentaje de participacion es mayor al 100%";
                }
            }
            else
            {
                return "El empleado al que quiere relacionar el beneficiario no existe";
            }
        }
        public async Task<List<BeneficiariosResponse>> ListarBeneficiarios()
        {
            List<BeneficiariosResponse> objres = new List<BeneficiariosResponse>();
            var res = await _objcone.Set<Beneficiarios>().FromSqlRaw("EXEC SP_ListarBeneficiarios").ToListAsync();

            if (res != null)
            {
                foreach (var obj in res)
                {
                    BeneficiariosResponse empleadosResponseCreate = new BeneficiariosResponse()
                    {
                        NumBeneficiario = obj.NumBeneficiario,
                        NumEmpleado = obj.NumEmpleado,
                        Nombre = obj.Nombre,
                        Apellidos = obj.Apellidos,
                        FechaNacimiento = obj.FechaNacimiento,
                        CURP = obj.CURP,
                        SSN = obj.SSN,
                        Telefono = obj.telefono,
                        Nacionalidad = obj.Nacionalidad,
                        PorcentajeParticipacion = obj.PorcentajeParticipacion,
                    };
                    objres.Add(empleadosResponseCreate);
                }
            }
            return objres;
        }
        public async Task<BeneficiariosResponse> ObtenerBeneficiarios(int objbeneficiario)
        {
            var parameter1Param = new SqlParameter("@Numbeneficiario", SqlDbType.Int)
            {
                Value = objbeneficiario
            };
            var Objbeneficiario = await _objcone.Set<Beneficiarios>().FromSqlRaw("EXEC SP_ListarBeneficiariosXId @Numbeneficiario", parameter1Param).ToListAsync();
            BeneficiariosResponse Objres = new BeneficiariosResponse();
            if (Objbeneficiario.Count > 0)
            {
                Objres.NumBeneficiario = Objbeneficiario[0].NumBeneficiario;
                Objres.NumEmpleado = Objbeneficiario[0].NumEmpleado;
                Objres.Nombre = Objbeneficiario[0].Nombre;
                Objres.Apellidos = Objbeneficiario[0].Apellidos;
                Objres.FechaNacimiento = Objbeneficiario[0].FechaNacimiento;
                Objres.CURP = Objbeneficiario[0].CURP;
                Objres.SSN = Objbeneficiario[0].SSN;
                Objres.Telefono = Objbeneficiario[0].telefono;
                Objres.Nacionalidad = Objbeneficiario[0].Nacionalidad;
                Objres.PorcentajeParticipacion = Objbeneficiario[0].PorcentajeParticipacion;
            }
            return Objres;
        }
        public async Task<bool> ActualizarBeneficiario(BeneficiariosRequestUpdate obj)
        {

            var existeBeneficiario = _objcone.Beneficiarios.AsNoTracking().FirstOrDefault(x => x.NumBeneficiario == obj.NumBeneficiario);

            if (existeBeneficiario != null)
            {
                Beneficiarios objbeneficiario = new Beneficiarios()
                {
                    NumBeneficiario = obj.NumBeneficiario,
                    NumEmpleado = obj.NumEmpleado,
                    Nombre = obj.Nombre,
                    Apellidos = obj.Apellidos,
                    FechaNacimiento = obj.FechaNacimiento,
                    CURP = obj.CURP,
                    SSN = obj.SSN,
                    telefono = obj.Telefono,
                    Nacionalidad = obj.Nacionalidad,
                    PorcentajeParticipacion = obj.PorcentajeParticipacion
                };
                _objcone.Beneficiarios.Update(objbeneficiario);
                await _objcone.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }
        public async Task<bool> EliminarBeneficiario(int Beneficiario)
        {

            var res = await _objcone.Beneficiarios.FindAsync(Beneficiario);

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
