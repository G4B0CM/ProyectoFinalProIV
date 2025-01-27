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

        public string CompetenciasInscritas { get; set; } = "";

        public List<int> GetCompetencias()
        {
            return string.IsNullOrEmpty(CompetenciasInscritas)
                ? new List<int>()
                : CompetenciasInscritas.Split(',').Select(int.Parse).ToList();
        }

        public void SetCompetencias(List<int> competencias)
        {
            CompetenciasInscritas = string.Join(",", competencias);
        }
    }

}
