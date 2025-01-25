using Avance2Progreso.Services;
using Avance2Progreso.Models;
using Avance2Progreso.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Avance2Progreso.ViewModels
{
    public class LoginPageViewModel : ObservableObject
    {
        private string _username;
        private string _password;
        private readonly UserService _userService;
        private List<User> Users;
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
                }
            }
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

        public ICommand NavigateToRegistroCommand { get; }
        public ICommand NavigateToAdmin { get; }
        public event Action ShowAlert;

        
        public LoginPageViewModel()
        {
            _userService = new UserService(new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5206/api/Users")
            });
            _user = new Models.User();
            Users = new List<User>();
            NavigateToRegistroCommand = new RelayCommand(OnNavigateToRegistro);
            NavigateToAdmin = new RelayCommand(OnNavigateToAdmin);
        }

        private async void OnNavigateToRegistro()
        {
            try
            {
                await Shell.Current.GoToAsync("registro");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private async void OnNavigateToAdmin()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                Users = users;
                var usuarioCoincidente = Users.FirstOrDefault(u => u.Username == Username);

                // Verifica si el usuario existe
                if (usuarioCoincidente == null)
                {
                    await Shell.Current.DisplayAlert("Alerta", "No existen usuarios con ese nombre", "OK");
                    return;
                }

                // Verifica si la contraseña ingresada coincide con la del usuario
                if (usuarioCoincidente.Password == Password)
                {
                    bool isAdmin = usuarioCoincidente.IsAdmin;

                    // Obtener una referencia a AppShell directamente
                    var appShell = (AppShell)Shell.Current;

                    // Cambiar las Tabs visibles dependiendo de si el usuario es admin o no
                    appShell.SetUserTabs(isAdmin);

                    if (usuarioCoincidente.IsAdmin)
                    {
                        await Shell.Current.GoToAsync("//AdminsHomePage");
                    }
                    else
                    {
                        await Shell.Current.GoToAsync("//StudentsHomePage");
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Alerta", $"Contraseña incorrecta para {Username}", "OK");
                }
            }
            catch (Exception ex)
            {
                // Captura el error y muestra más detalles
                Console.WriteLine($"Error: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un problema al intentar iniciar sesión", "OK");
            }
        }

    }
}