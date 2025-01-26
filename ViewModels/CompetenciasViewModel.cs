using System.Collections.ObjectModel;
using System.Windows.Input;
using Avance2Progreso.Models;
using Avance2Progreso.Repositories;
using CommunityToolkit.Mvvm.ComponentModel;
using Avance2Progreso.ViewModels;
using CommunityToolkit.Mvvm.Input;



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

        public DateTime FechaCreacion
        {
            get => _competencia.FechaCreacion;
            set
            {
                if (_competencia.FechaCreacion != value)
                {
                    _competencia.FechaCreacion = value;
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

            GuardarCommand = new AsyncRelayCommand(Guardar);
            ListarCommand = new AsyncRelayCommand(ListarCompetencias);
            EliminarCommand = new AsyncRelayCommand(EliminarCompetencias);
            //BuscarPorNombreCommand = new RelayCommand();

        }
        private async Task Guardar()
        {
            try
            {
                if (string.IsNullOrEmpty(_competencia.Nombre))
                {
                    throw new Exception("El nombre no puede estar vacío.");
                }
                if (string.IsNullOrEmpty(_competencia.Categoria))
                {
                    throw new Exception("El nombre no puede estar vacío.");
                }
                if (string.IsNullOrEmpty(_competencia.Descripcion))
                {
                    throw new Exception("El nombre no puede estar vacío.");
                }

                _competenciaRepository.GuardarCompetencia(_competencia.Nombre, _competencia.Categoria,_competencia.Descripcion);

                await Shell.Current.DisplayAlert("Alerta", $"Competencia {_competencia.Nombre} guardada exitosamente.","OK");
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al guardar la competencia. Detalles: {ex.Message}";
            }
        }

        private async Task ListarCompetencias()
        {
            try
            {
                var competencias = await _competenciaRepository.ListarCompetencias();
                Competencias.Clear();
                foreach (var competencia in competencias)
                {
                    Competencias.Add(competencia);
                }
                StatusMessage = $"Las competencias se cargaron correctamente";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al cargar las competencias. Detalles: {ex.Message}";
            }
        }


        private async Task EliminarCompetencias()
        {
            try
            {
                if (Id <= 0 )
                    throw new Exception("Usuario no válido para eliminar.");

                _competenciaRepository.EliminarCompetencia(Id);
                var competencia = Competencias.First(Competencia => Competencia.Id == Id);
                Competencias.Remove(competencia);

                StatusMessage = $"Usuario {competencia.Nombre} eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al eliminar el usuario: {ex.Message}";
            }
        }

    }
}
