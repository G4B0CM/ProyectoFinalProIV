using Avance2Progreso.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avance2Progreso.Interfaces
{
    public interface IAdministradorRepository
    {
        bool CrearCompetencia(Competencias competencia);
        Competencias DevuelveCompetencia();
        IEnumerable<Competencias> ListarCompetencias();
        bool CrearUsuarioEstudiante(User estudiante);
        IEnumerable<User> ListarUsuarios();

        
    }
}
