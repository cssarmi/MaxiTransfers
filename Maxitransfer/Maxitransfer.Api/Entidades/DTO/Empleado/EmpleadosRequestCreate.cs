using System;

namespace MaxiTransfers.Api.Entidades.DTO.Empleado
{
    public class EmpleadosRequestCreate
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string CURP { get; set; }
        public string SSN { get; set; }
        public long Telefono { get; set; }
        public string Nacionalidad { get; set; }
    }
}
