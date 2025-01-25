
using SQLite;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Avance2Progreso.Models
{
    [SQLite.Table("Competencias")]
    public class Competencias
    {
        [PrimaryKey, AutoIncrement]
        [SQLite.Column("Id")]
        public int Id { get; set; }
        [Required(ErrorMessage ="Nombre Requerido")]
        [SQLite.Column("Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage ="Categoria Requerida")]
        [SQLite.Column("Categoria")]
        public string Categoria { get; set; }
        [Required(ErrorMessage ="Descripción Requerida")]
        [SQLite.Column("Descripción")]
        public string Descripcion { get; set; }
        [SQLite.Column("FechaCreación")]
        public DateTime FechaCreación { get; set; } 
    }
}
