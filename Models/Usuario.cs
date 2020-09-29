using System;
using System.Collections.Generic;

namespace Clinica.Models
{
    public partial class Usuario
    {
        public string UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool Foto { get; set; }
        public string Clave { get; set; }
    }
}
