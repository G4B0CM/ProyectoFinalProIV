
using SQLite;
using System.ComponentModel.DataAnnotations;

namespace Avance2Progreso.Models
{
    [SQLite.Table("Competencias")]
    public class Competencias
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Required(ErrorMessage ="Nombre Requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage ="Categoria Requerida")]
        public string Categoria { get; set; }
        [Required(ErrorMessage ="Descripción Requerida")]
        public string Descripcion { get; set; }
        public DateTime FechaCreación { get; set; } 
    }
}
