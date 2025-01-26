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
        public ICommand EditarCompetenciaCommand { get; set; }
        public ICommand LimpiarBusquedaCommand { get; set; }



        public Competencias Competencia
        {
            get => _competencia;
            set
            {

                if (SetProperty(ref _competencia, value))
                {
                    OnPropertyChanged(nameof(Nombre));
                    OnPropertyChanged(nameof(Categoria));
                    OnPropertyChanged(nameof(Descripcion));
                    OnPropertyChanged(nameof(FechaCreacion));

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
            get => _competencia?.Nombre ?? string.Empty; // Evitar NullReferenceException
            set
            {
                if (_competencia == null) _competencia = new Competencias(); // Inicializar si es null
                if (_competencia.Nombre != value)
                {
                    _competencia.Nombre = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Categoria
        {
            get => _competencia?.Categoria ?? string.Empty; // Evitar NullReferenceException
            set
            {
                if (_competencia == null) _competencia = new Competencias(); // Inicializar si es null
                if (_competencia.Categoria != value)
                {
                    _competencia.Categoria = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Descripcion
        {
            get => _competencia?.Descripcion ?? string.Empty; // Evitar NullReferenceException
            set
            {
                if (_competencia == null) _competencia = new Competencias(); // Inicializar si es null
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
            BuscarPorNombreCommand = new AsyncRelayCommand(BuscarCompetenciaPorNombre);
            EditarCompetenciaCommand = new AsyncRelayCommand(EditarCompetencia);
            LimpiarBusquedaCommand = new AsyncRelayCommand(LimpiarBusqueda);
        }
        private async Task Guardar()
        {
            try
            {
                if (string.IsNullOrEmpty(Nombre))
                {
                    throw new Exception("El nombre no puede estar vacío.");
                }
                if (string.IsNullOrEmpty(Categoria))
                {
                    throw new Exception("El nombre no puede estar vacío.");
                }
                if (string.IsNullOrEmpty(Descripcion))
                {
                    throw new Exception("El nombre no puede estar vacío.");
                }

                _competenciaRepository.GuardarCompetencia(Nombre, Categoria,Descripcion);

                await Shell.Current.DisplayAlert("Alerta", $"Competencia {Nombre} guardada exitosamente.","OK");
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al guardar la competencia. Detalles: {ex.Message}";
                await Shell.Current.DisplayAlert("Error", StatusMessage, "Ok");
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
                if (Competencia == null)
                {
                    StatusMessage = "Seleccione una competencia para eliminar.";
                    await Shell.Current.DisplayAlert("Error", StatusMessage, "OK");
                    return;
                }

                int filasEliminadas = await _competenciaRepository.EliminarCompetencia(Competencia.Id);

                if (filasEliminadas > 0)
                {
                    var competencia = Competencias.FirstOrDefault(c => c.Id == Competencia.Id);
                    if (competencia != null)
                    {
                        Competencias.Remove(competencia);
                    }
                    StatusMessage = $"Competencia '{Competencia.Nombre}' eliminada exitosamente.";
                    Competencia = null; // Reinicia la selección
                }
                else
                {
                    StatusMessage = "No se pudo eliminar la competencia. Verifique si aún existe.";
                }

                await Shell.Current.DisplayAlert("Resultado", StatusMessage, "OK");
            
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al eliminar la competencia: {ex.Message}";
                await Shell.Current.DisplayAlert("Error", StatusMessage, "OK");
            }
        }
       

        private async Task BuscarCompetenciaPorNombre()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Nombre))
                {
                    await Shell.Current.DisplayAlert("Error", "Ingrese un nombre para buscar.", "OK");
                    return;
                }

                // Llama al repositorio para buscar la competencia
                var competenciaEncontrada = await _competenciaRepository.BuscarCompetenciaPorNombre(Nombre);

                if (competenciaEncontrada != null)
                {
                    Competencia = competenciaEncontrada;

                    // Muestra los detalles de la competencia encontrada
                    await Shell.Current.DisplayAlert(
                        "Competencia Encontrada",
                        $"Nombre: {Competencia.Nombre}\n" +
                        $"Categoría: {Competencia.Categoria}\n" +
                        $"Descripción: {Competencia.Descripcion}\n" +
                        $"Fecha de Creación: {Competencia.FechaCreacion:dd/MM/yyyy}",
                        "OK"
                    );

                    // Actualiza la lista para mostrar solo la competencia encontrada
                    Competencias.Clear();
                    Competencias.Add(competenciaEncontrada);

                    StatusMessage = "Competencia encontrada correctamente.";
                }
                else
                {
                    await Shell.Current.DisplayAlert("Aviso", "No se encontró una competencia con ese nombre.", "OK");
                    StatusMessage = "No se encontró la competencia.";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al buscar la competencia: {ex.Message}";
                await Shell.Current.DisplayAlert("Error", StatusMessage, "OK");
            }
        }

        private async Task EditarCompetencia()
        {
            try
            {
                if (Competencia == null)
                {
                    StatusMessage = "Seleccione una competencia para editar.";
                    await Shell.Current.DisplayAlert("Error", StatusMessage, "OK");
                    return;
                }

                // Mostrar alertas para capturar los nuevos valores
                string nuevoNombre = await Shell.Current.DisplayPromptAsync(
                    "Editar Competencia",
                    "Ingrese el nuevo nombre:",
                    initialValue: Competencia.Nombre);

                string nuevaCategoria = await Shell.Current.DisplayPromptAsync(
                    "Editar Competencia",
                    "Ingrese la nueva categoría:",
                    initialValue: Competencia.Categoria);

                string nuevaDescripcion = await Shell.Current.DisplayPromptAsync(
                    "Editar Competencia",
                    "Ingrese la nueva descripción:",
                    initialValue: Competencia.Descripcion);

                // Validar los valores ingresados
                if (string.IsNullOrEmpty(nuevoNombre) || string.IsNullOrEmpty(nuevaCategoria) || string.IsNullOrEmpty(nuevaDescripcion))
                {
                    StatusMessage = "Todos los campos son obligatorios.";
                    await Shell.Current.DisplayAlert("Error", StatusMessage, "OK");
                    return;
                }

                // Actualizar en el repositorio
                _competenciaRepository.EditarCompetencia(Competencia.Id,nuevoNombre, nuevaCategoria, nuevaDescripcion);

                // Refrescar la lista completa
                await ListarCompetencias();

                StatusMessage = $"Competencia '{nuevoNombre}' actualizada correctamente.";
                await Shell.Current.DisplayAlert("Éxito", StatusMessage, "OK");
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al editar la competencia: {ex.Message}";
                await Shell.Current.DisplayAlert("Error", StatusMessage, "OK");
            }
        }
        private async Task LimpiarBusqueda()
        {
            Nombre = string.Empty;
            Categoria = string.Empty;
            Descripcion = string.Empty;
        }

    }
}
