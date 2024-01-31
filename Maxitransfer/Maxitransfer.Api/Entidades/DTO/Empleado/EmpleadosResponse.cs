using System;
using System.Numerics;

namespace MaxiTransfers.Api.Entidades.DTO.Empleado
{
    public class EmpleadosResponse
    {
        public int NumEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string CURP { get; set; }
        public string SSN { get; set; }
        public long Telefono { get; set; }
        public string Nacionalidad { get; set; }
    }
}
