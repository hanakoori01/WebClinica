using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClinica.Models.ViewModel
{
    public class TipoUsuarioPagina
    {
        public int Tipousuariopaginaid { get; set; }
        public int? Tipousuarioid { get; set; }
        public int? Paginaid { get; set; }
        public int? Bhabilitado { get; set; }
        public string NombreTipoUsuario { get; set; }
        public string NombrePagina { get; set; }
    }
}
