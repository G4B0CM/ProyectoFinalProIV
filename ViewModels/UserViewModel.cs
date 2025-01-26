using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Avance2Progreso.Repositories;
namespace Avance2Progreso.ViewModels
{
    public class UserViewModel : ObservableObject, IQueryAttributable
    {
        private ObservableCollection<Models.User> _peopleList;
        private string _statusMessage;
        private readonly UserRepository _userRepository;

        public ICommand SaveCommand { get; }
        public ICommand GetAllPeopleCommand { get; }
        public ICommand DeletePersonCommand { get; }

        private Models.User _user;

        public Models.User user
        {
            get => _user;
            set
            {
                if (SetProperty(ref _user, value))
                {
                    OnPropertyChanged(nameof(Username));
                    OnPropertyChanged(nameof(Password));
                    OnPropertyChanged(nameof(IsAdmin));
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        public ObservableCollection<Models.User> UsersList
        {
            get => _peopleList;
            set => SetProperty(ref _peopleList, value);
        }

        public string Username
        {
            get => _user.Username;
            set
            {
                if (_user.Username != value)
                {
                    _user.Username = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get => _user.Password;
            set
            {
                if (_user.Password != value)
                {
                    _user.Password = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsAdmin
        {
            get => _user.IsAdmin;
            set
            {
                if (_user.IsAdmin != value)
                {
                    _user.IsAdmin = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Id => _user.Id;

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public UserViewModel()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EduGaboEmi.db3");
            _userRepository = new UserRepository(dbPath);

            _user = new Models.User();
            UsersList = new ObservableCollection<Models.User>();
            SaveCommand = new AsyncRelayCommand(Save);
            GetAllPeopleCommand = new AsyncRelayCommand(LoadPeople);
            DeletePersonCommand = new AsyncRelayCommand<Models.User>(Eliminar);
        }

        private async Task Save()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                    throw new Exception("El nombre y la contraseña no pueden estar vacíos.");

                _userRepository.AddNewPerson(Username, Password, IsAdmin);

                StatusMessage = $"Usuario {Username} guardado exitosamente.";
                await LoadPeople(); // Recargar la lista
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al guardar el usuario: {ex.Message}";
            }
        }

        private async Task Eliminar(Models.User userToDelete)
        {
            try
            {
                if (userToDelete == null || userToDelete.Id <= 0)
                    throw new Exception("Usuario no válido para eliminar.");

                _userRepository.EliminarPersona(userToDelete.Id);
                UsersList.Remove(userToDelete);

                StatusMessage = $"Usuario {userToDelete.Username} eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al eliminar el usuario: {ex.Message}";
            }
        }

        private async Task LoadPeople()
        {
            try
            {
                var users = _userRepository.GetAllPeople();
                UsersList.Clear();
                foreach (var user in users)
                {
                    UsersList.Add(user);
                }

                StatusMessage = $"Se cargaron {UsersList.Count} usuarios.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al obtener usuarios: {ex.Message}";
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("user") && query["user"] is Models.User userFromQuery)
            {
                user = userFromQuery;
            }
            else if (query.ContainsKey("deleted"))
            {
                string username = query["deleted"].ToString();
                var matchedUser = UsersList.FirstOrDefault(u => u.Username == username);

                if (matchedUser != null)
                    UsersList.Remove(matchedUser);
            }
        }
        public async Task InscribirseEnCompetencia(int competenciaId)
        {
            try
            {
                if (_user.IsAdmin)
                {
                    StatusMessage = "Los administradores no pueden inscribirse en competencias.";
                    return;
                }

                if (_user.CompetenciasInscritas.Contains(competenciaId))
                {
                    StatusMessage = "Ya estás inscrito en esta competencia.";
                    return;
                }

                _user.CompetenciasInscritas.Add(competenciaId);
                StatusMessage = "Inscripción exitosa.";

                // Aquí se guardaría la actualización en SQLite o se enviaría a la API en el futuro
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error al inscribirse: {ex.Message}";
            }
        }

    }
}
