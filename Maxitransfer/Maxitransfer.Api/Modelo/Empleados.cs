﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace MaxiTransfers.Api.Modelo
{
    public class Empleados
    {
        [Key]
        public int NumEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string CURP { get; set; }
        public string SSN { get; set; }
        public long telefono { get; set; }
        public string Nacionalidad { get; set; }

    }
}