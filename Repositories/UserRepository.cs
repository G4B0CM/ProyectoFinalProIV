using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avance2Progreso.Models;

namespace Avance2Progreso.Repositories
{
    public class UserRepository
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        private SQLiteConnection conn;

        private void Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<User>();
        }

        public UserRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void AddNewPerson(string name,string password,bool isAdmin)
        {
            int result = 0;
            try
            {
                Init();

                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                if (string.IsNullOrEmpty(password))
                    throw new Exception("Valid password required");

                result = conn.Insert(new User { Username = name, Password = password , IsAdmin = false });

                StatusMessage = string.Format("{0} usuario añadido (Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al agregar {0}. Error: {1}", name, ex.Message);
            }

        }

        public List<User> GetAllPeople()
        {
            try
            {
                Init();
                return conn.Table<User>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<User>();
        }
        public void EliminarPersona(int id)
        {
            int result = 0;
            try
            {
                Init();

                if (id!=0)
                    throw new Exception("Es requerida una Id valida");
                var person = conn.Table<Models.User>().FirstOrDefault(p => p.Id == id);
                result = conn.Delete(person);

                StatusMessage = string.Format("{0} record(s) deleted (Name: {1})", result, id);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to delete {0}. Error: {1}", id, ex.Message);
            }
        }

        public void EditarPersona(int id, string name, string password, bool isAdmin)
        {
            int result = 0;
            try
            {
                Init();

                if (id <= 0)
                    throw new Exception("Se requiere un Id válido.");

                // Buscar el usuario por ID
                var person = conn.Table<User>().FirstOrDefault(p => p.Id == id);

                if (person == null)
                    throw new Exception($"Usuario con Id {id} no encontrado.");

                person.Username = name ?? person.Username; //Profe el ?? indica que si es null se mantiene
                person.Password = password ?? person.Password; //el valor existente
                person.IsAdmin = isAdmin;

                result = conn.Update(person);

                StatusMessage = string.Format("{0} registro(s) actualizado(s) (Id: {1}, Name: {2})", result, id, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al actualizar el usuario con Id {0}. Error: {1}", id, ex.Message);
            }
        }
    }
}
