using SQLite;

namespace Avance2Progreso.Models
{
    [SQLite.Table ("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Username { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public List<int> CompetenciasInscritas { get; set; } = new List<int>();
    }

}
