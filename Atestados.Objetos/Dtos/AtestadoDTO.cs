using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestados.Objetos.Dtos
{
    public class AtestadoDTO
    {
        public int AtestadoID { get; set; }
        public string Nombre { get; set; }
        public int NumeroAutores { get; set; }
        public string Observaciones { get; set; }
        public DateTime HoraCreacion { get; set; }
        public Nullable<int> Enviado { get; set; }
        public Nullable<int> Descargado { get; set; }
        public Nullable<int> CantidadHoras { get; set; }
        public string Lugar { get; set; }
        public string CatalogoTipo { get; set; }
        public string Enlace { get; set; }
        public string Pais { get; set; }
        public string Rubro { get; set; }
        public string Persona { get; set; }
    }
}
