using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atestados.Objetos.Dtos
{
    public class AutorDTO
    {
        public int AtestadoID { get; set; }
        public int PersonaID { get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Email { get; set; }
        public double Porcentaje { get; set; }
        public string Departamento { get; set; }

    }
}
