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
        public int Enviado { get; set; }
        public int Descargado { get; set; }
        public int CantidadHoras { get; set; }
        public string Lugar { get; set; }
        public string CatalogoTipo { get; set; }
        public string Enlace { get; set; }
        public int RubroID { get; set; }
        public int PaisID { get; set; }
        public int PersonaID { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }

        public List<ArchivoDTO> Archivos { get; set; }
        public PaisDTO Pais { get; set; }
        public PersonaDTO Persona { get; set; }
        public RubroDTO Rubro { get; set; }
        public List<AtestadoXPersonaDTO> AtestadoXPersona { get; set; }
        public DominioIdiomaDTO DominioIdioma { get; set; }
        public FechaDTO Fecha { get; set; }
        public InfoEditorialDTO InfoEditorial { get; set; }
    }
}
