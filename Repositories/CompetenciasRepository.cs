using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Avance2Progreso.Models;
using Microsoft.Maui.Controls;
using SQLite;

namespace Avance2Progreso.Repositories
{
    public class CompetenciasRepository
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        private SQLiteConnection conn;

        private void Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Competencias>();
            

        }
        public CompetenciasRepository(string dbPath)
        {
            _dbPath = dbPath;
        }
        public async Task GuardarCompetencia(string nombre, string categoria, string descripcion)
        {
            try
            {
                Init();
                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("Se requiere un nombre valido");
                if (string.IsNullOrEmpty(categoria))
                    throw new Exception("Se requiere una region valida");
                if (string.IsNullOrEmpty(descripcion))
                    throw new Exception("Se requiere una region valida");

                var result = conn.Insert(new Competencias
                {
                    Nombre = nombre,
                    Categoria = categoria,
                    Descripcion = descripcion,
                    FechaCreacion = ObtenerFecha()
                });

                StatusMessage = $"Competencia {nombre} guardada correctamente";


            } catch (Exception ex) {
                StatusMessage = $"Error al guardar la competencia. Detalles: {ex.Message}";
            }
        }
        public async Task<List<Competencias>> ListarCompetencias()
        {
            try
            {
                Init() ;
                return conn.Table<Competencias>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al listar las competencias";
            }
            return new List<Competencias>();
        }

        public static DateTime ObtenerFecha()
        {
            return DateTime.Now;
        }

        public async Task<int> EliminarCompetencia(int id)
        {
            try
            {
                Init();
                var competencia = conn.Table<Competencias>().FirstOrDefault(n => n.Id == id);
                if (competencia != null)
                {
                    return conn.Delete(competencia);
                }
                StatusMessage = $"No se encontró una competencia con ID: {id}";
                return 0;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al eliminar la competencia. DEtalles: {ex.Message}";
                return 0;
            }
        }

        public async Task<Competencias> BuscarCompetenciaPorNombre(string nombre)
        {
            try
            {
                Init();
                var competencia = conn.Table<Competencias>().FirstOrDefault(c => c.Nombre == nombre);
                if(competencia == null)
                {
                    StatusMessage = $"No se encontro ninguna competencia con el nombre {nombre}";
                }
                return competencia;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al buscar la competencia. Detalles: {ex.Message}";
                return null;
            }
        }

        public void EditarCompetencia(int id, string nombre, string categoria, string descripcion)
        {
            int result = 0;
            try
            {
                Init();

                if (id <= 0)
                    throw new Exception("Se requiere un ID válido para editar la competencia.");

                // Buscar la competencia por ID
                var competencia = conn.Table<Competencias>().FirstOrDefault(p => p.Id == id);

                if (competencia == null)
                    throw new Exception($"No se encontró una competencia con el ID: {id}");

                // Actualizar los valores
                competencia.Nombre = nombre ?? competencia.Nombre;
                competencia.Categoria = categoria ?? competencia.Categoria;
                competencia.Descripcion = descripcion ?? competencia.Descripcion;

                // Guardar los cambios
                conn.Update(competencia);

                StatusMessage = $"Competencia con ID {id} actualizada correctamente.";
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al actualizar el usuario con Nombre {0}. Error: {1}", nombre, ex.Message);
            }
        }

    }
}
