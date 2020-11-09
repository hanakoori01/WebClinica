using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClinica.Models.ViewModel
{
    public class TipoUsuarioPaginas
    {
        public int TipoUsuarioPaginaId { get; set; }
        public int? TipousuarioId { get; set; }
        public int? PaginaId { get; set; }
        public int? BotonHabilitado { get; set; }
        public string NombreTipoUsuario { get; set; }
        public string NombrePagina { get; set; }
    }
}
