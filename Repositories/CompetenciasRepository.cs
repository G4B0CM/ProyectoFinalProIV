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
            {
                return;

                conn = new SQLiteConnection(_dbPath);
                conn.CreateTable<Competencias>();
            }

        }
        public CompetenciasRepository(string dbPath)
        {
            _dbPath = dbPath;
        }
        public async Task SaveCompetencia(string nombre, string categoria, string descripcion)
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
                    FechaCreación = ObtenerFecha()
                });

                StatusMessage = $"Competencia {nombre} guardada correctamente";


            } catch (Exception ex) {
                StatusMessage = $"Error al guardar la competencia. Detalles: {ex.Message}";
            }
        }
        public async Task<List<Competencias>> GetAllCompetencias()
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


    }
}
