﻿using Atestados.Datos.Modelo;
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
        [StringLength(250)]
        [Required(ErrorMessage = "Este es un campo requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este es un campo requerido")]
        [DisplayName("Número de autores")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ser un número mayor a {1}")]
        public int NumeroAutores { get; set; }
        [StringLength(1000)]
        public string Observaciones { get; set; }
        public DateTime HoraCreacion { get; set; }
        [StringLength(250)]
        [Url]
        public string Enlace { get; set; }
        [Url]
        [StringLength(250)]
        [Required(ErrorMessage = "Este es un campo requerido")]
        public string Website { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Este es un campo requerido")]
        public string Editorial { get; set; }
        [Required(ErrorMessage = "Este es un campo requerido")]
        [DataType(DataType.Date)]
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
