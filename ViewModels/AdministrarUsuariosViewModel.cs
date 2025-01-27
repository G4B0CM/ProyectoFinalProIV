using Avance2Progreso.Services;
using Avance2Progreso.Repositories;
using Avance2Progreso.Models;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;


namespace Avance2Progreso.ViewModels
{
    internal class AdministrarUsuariosViewModel : ObservableObject
    {
        private ObservableCollection<Models.User> _peopleList;
        private string _statusMessage;
        private readonly UserService _userService;
        private readonly UserRepository _userRepository;

        public ICommand SaveCommand { get; }
        public ICommand GuardarDesdeAdminCommand { get; }
        public ICommand GetAllPeopleCommand { get; }
        public ICommand DeletePersonCommand { get; }
        public ICommand ObtenerUnaPersonaCommand {  get; }
        public ICommand ActualizarPersonaCommand { get; }
        public ICommand RegresarLoginCommand { get; }

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

        public int Id
        {
            get => _user.Id;
            set
            {
                if (_user.Id != value)
                {
                    _user.Id = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public AdministrarUsuariosViewModel()
        {
            _userService = new UserService(new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5206/api/Users")
            });

            _user = new Models.User();
            UsersList = new ObservableCollection<Models.User>();
            SaveCommand = new AsyncRelayCommand(Save);
            GuardarDesdeAdminCommand = new AsyncRelayCommand(GuardarDesdeAdmin);
            GetAllPeopleCommand = new AsyncRelayCommand(LoadPeople);
            DeletePersonCommand = new AsyncRelayCommand(Eliminar);
            ObtenerUnaPersonaCommand = new AsyncRelayCommand(CargarUnaPersona);
            ActualizarPersonaCommand = new AsyncRelayCommand(ActualizarPersona);
            RegresarLoginCommand = new AsyncRelayCommand(IrALogin);
        }

        private async Task Save()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "El usuario y contraseña son obligatorios.", "OK");
                    return;
                }

                var newUser = new User
                {
                    Username = Username,
                    Password = Password,
                    IsAdmin = false
                };

                var createdUser = await _userService.CreateUserAsync(newUser);
                await Application.Current.MainPage.DisplayAlert("Usuario Creado", $"Usuario creado con ID: {createdUser.Id}", "OK");

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Un error occurrio: {ex.Message}", "OK");
            }   
        }

        private async Task GuardarDesdeAdmin()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "El usuario y contraseña son obligatorios.", "OK");
                    return;
                }

                var newUser = new User
                {
                    Username = Username,
                    Password = Password,
                    IsAdmin = IsAdmin
                };

                var createdUser = await _userService.CreateUserAsync(newUser);
                await Application.Current.MainPage.DisplayAlert("Usuario Creado", $"Usuario creado con ID: {createdUser.Id}", "OK");

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Un error occurrio: {ex.Message}", "OK");
            }
        }

        private async Task Eliminar()
        {
            try
            {
                if (Id>0)
                {
                    await _userService.DeleteUserAsync(Id);
                    await Application.Current.MainPage.DisplayAlert("User Deleted", $"Usuario con ID {Id} fue eliminado.", "OK");

                   
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Please enter a valid User ID.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async Task LoadPeople()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                UsersList = new ObservableCollection<User>(users); 
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async Task CargarUnaPersona()
        {
            try
            {
                if (Id > 0)
                {
                    var user = await _userService.GetUserByIdAsync(Id);
                    Username = $"ID: {user.Id}, Username: {user.Username}, IsAdmin: {user.IsAdmin}";
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Please enter a valid User ID.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
            
        }
        private async Task ActualizarPersona()
        {
            try
            {
                if (Id>0)
                {
                    var updatedUser = new User
                    {
                        Id = Id,
                        Username = Username,
                        Password = Password,
                        IsAdmin = IsAdmin
                    };

                    await _userService.UpdateUserAsync(Id, updatedUser);
                    await Application.Current.MainPage.DisplayAlert("Usuario Actualizado", $"El usuario con Id {Id} fue actualizado.", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Please enter a valid User ID.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
        private async Task IrALogin()
        {
            try
            {
                await Shell.Current.GoToAsync("//login");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }


    }
}
