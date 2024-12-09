using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avance2Progreso.Models
{
    public static class ListaUsuarios
    {
        public static List<User> Users = new List<User>
    {
        new User { Username = "admin", Password = "12345", Role = "Admin" },
        new User { Username = "estudiante", Password = "12345", Role = "Student" }
    };

        public static User Authenticate(string username, string password)
        {
            return Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
