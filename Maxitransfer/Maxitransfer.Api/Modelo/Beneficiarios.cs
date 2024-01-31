using System;
using System.ComponentModel.DataAnnotations;

namespace MaxiTransfers.Api.Modelo
{
    public class Beneficiarios
    {
        [Key]
        public int NumBeneficiario { get; set; }
        public int NumEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string CURP { get; set; }
        public string SSN { get; set; }
        public long telefono { get; set; }
        public string Nacionalidad { get; set; }
        public decimal PorcentajeParticipacion { get; set; }

    }
}
