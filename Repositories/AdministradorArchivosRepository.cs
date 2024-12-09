using Avance2Progreso.Interfaces;
using Avance2Progreso.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avance2Progreso.Repositories
{
    public class AdministradorArchivosRepository : IAdministradorRepository
    {
        private string _fileCompetencias = Path.Combine(FileSystem.AppDataDirectory, "competencias.txt"); //Documento con información Competencias
        private string _fileUsuarios = Path.Combine(FileSystem.AppDataDirectory, "usuarios.txt"); //Documento con información Usuarios

        // Crear una nueva competencia
        
        public bool CrearCompetencia(Competencias competencia)
        {
            try
            {
                string json_data = JsonConvert.SerializeObject(competencia);
                File.WriteAllText(_fileCompetencias, json_data);
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public Competencias DevuelveCompetencia()
        {
            Competencias competencia = new Competencias();
            if (File.Exists(_fileCompetencias))
            {
                string json_data = File.ReadAllText(_fileCompetencias);
                competencia = JsonConvert.DeserializeObject<Competencias>(json_data);
            }

            return competencia;
        }

        // Listar todas las competencias
        public IEnumerable<Competencias> ListarCompetencias()
        {
            try
            {
                // Valida que el archivo existe
                if (File.Exists(_fileCompetencias))
                {
                    //Lee y deserializa
                    string json_data = File.ReadAllText(_fileCompetencias);
                    return JsonConvert.DeserializeObject<List<Competencias>>(json_data) ?? new List<Competencias>();
                }

                return new List<Competencias>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Crear un usuario tipo estudiante
        public bool CrearUsuarioEstudiante(User estudiante)
        {
            try
            {
                List<User> usuarios = ListarUsuarios().ToList();// Leer usuarios existentes
                usuarios.Add(estudiante);// Agregar el nuevo estudiante

                // Guardar la lista actualizada en el archivo
                string json_data = JsonConvert.SerializeObject(usuarios);
                File.WriteAllText(_fileUsuarios, json_data);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        // Listar todos los usuarios
        public IEnumerable<User> ListarUsuarios()
        {
            try
            {
                // Valida si el archivo existe
                if (File.Exists(_fileUsuarios))
                {
                    //Lee y deserializar
                    string json_data = File.ReadAllText(_fileUsuarios);
                    return JsonConvert.DeserializeObject<List<User>>(json_data) ?? new List<User>();
                }

                return new List<User>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
