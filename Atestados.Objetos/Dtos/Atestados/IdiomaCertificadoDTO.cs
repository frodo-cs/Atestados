using Atestados.Datos.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Atestados.Objetos.Dtos
{
    public class IdiomaCertificadoDTO
    {
        public int AtestadoID { get; set; }
        [DisplayName("Nombre del libro")]
        public string Nombre { get; set; }
        public string Observaciones { get; set; }
        public string Enlace { get; set; }
        [DisplayName("Año")]
        public DateTime Annio { get; set; }
        [DisplayName("País")]
        public int PaisID { get; set; }
        public Pais Pais { get; set; }
        public Persona Persona { get; set; }
        public int RubroID { get; set; }
        [DisplayName("Persona")]
        public int PersonaID { get; set; }

        public string Idioma { get; set; }
        public int Lectura { get; set; }
        public int Escrito { get; set; }
        public int Auditiva { get; set; }
        public int Oral { get; set; }
        public string Atestado { get; set; }

        public List<ArchivoDTO> Archivos { get; set; }
    }
}
