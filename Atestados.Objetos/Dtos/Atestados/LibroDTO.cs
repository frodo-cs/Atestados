using Atestados.Datos.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestados.Objetos.Dtos
{
    public class LibroDTO
    {
        public int AtestadoID { get; set; }
        [DisplayName("Nombre del libro")]
        public string Nombre { get; set; }
        [DisplayName("Número de autores")]
        public int NumeroAutores { get; set; }
        public string Observaciones { get; set; }
        public DateTime HoraCreacion { get; set; }
        public string Enlace { get; set; }
        public string Website { get; set; }
        public string Editorial { get; set; }
        [DisplayName("Año")]
        public DateTime Annio { get; set; }
        [DisplayName("País")]
        public int PaisID { get; set; }
        public Pais Pais { get; set; }
        public Persona Persona { get; set; }
        public int RubroID { get; set; }
        [DisplayName("Persona")]
        public int PersonaID { get; set; }

        public List<ArchivoDTO> Archivos { get; set; }
        public List<AutorDTO> Autores { get; set; }
        public List<AtestadoXPersonaDTO> AtestadoXPersona { get; set; }

    }
}
