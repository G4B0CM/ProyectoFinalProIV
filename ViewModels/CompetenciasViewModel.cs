using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avance2Progreso.Models;
using Avance2Progreso.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using Avance2Progreso.ViewModels;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;

namespace Avance2Progreso.ViewModels
{
    public class CompetenciasViewModel : ObservableObject
    {
        private Competencias _competencia;
        private readonly CompetenciasRepository _competenciaRepository;
        private string _statusMessage;
        public ObservableCollection<Competencias> Competencias { get; set; }


        public ICommand GuardarCommand { get; set; }
        public ICommand ListarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand BuscarPorNombreCommand { get; set; }


        public Competencias Competencia
        {
            get => _competencia;
            set
            {

                if (SetProperty(ref _competencia, value))
                {
                    OnPropertyChanged(nameof(_competencia.Nombre));
                    OnPropertyChanged(nameof(_competencia.Categoria));
                    OnPropertyChanged(nameof(_competencia.Descripcion));
                    OnPropertyChanged(nameof(_competencia.FechaCreacion));
                }
            }
        }
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }
        public string Nombre
        {
            get => _competencia.Nombre;
            set
            {
                if (_competencia.Nombre != value)
                {
                    _competencia.Nombre = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Categoria
        {
            get => _competencia.Categoria;
            set
            {
                if (_competencia.Categoria != value)
                {
                    _competencia.Categoria = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Descripcion
        {
            get => _competencia.Descripcion;
            set
            {
                if (_competencia.Descripcion != value)
                {
                    _competencia.Descripcion = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FechaCreacion
        {
            get => _competencia.Nombre;
            set
            {
                if (_competencia.Nombre != value)
                {
                    _competencia.Nombre = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Id => _competencia.Id;
        public CompetenciasViewModel()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "GabrielCalderon.db3");
            _competenciaRepository = new CompetenciasRepository(dbPath);
            _competencia = new Competencias();
            Competencias = new ObservableCollection<Competencias>();

            GuardarCommand = new AsyncRelayCommand();
            ListarCommand = new AsyncRelayCommand();
            EliminarCommand = new AsyncRelayCommand();
            BuscarPorNombreCommand = new RelayCommand();

        }




    }
}
