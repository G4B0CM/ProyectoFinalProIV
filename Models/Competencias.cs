using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avance2Progreso.Models
{
    public class Competencias
    {
        [Key]
        public string Id { get; set; }
        [Required(ErrorMessage ="Nombre Requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage ="Categoria Requerida")]
        public string Categoria { get; set; }
        [Required(ErrorMessage ="Descripción Requerida")]
        public string Descripción { get; set; }
    }
}
