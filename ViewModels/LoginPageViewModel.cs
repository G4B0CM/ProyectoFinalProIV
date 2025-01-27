using Avance2Progreso.Services;
using Avance2Progreso.Models;
using Avance2Progreso.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Security.Cryptography;
using System.Text;

namespace Avance2Progreso.ViewModels
{
    public class LoginPageViewModel : ObservableObject
    {
        private readonly string _encryptionKey = "EduGaboEmi";
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

                if (usuarioCoincidente == null)
                {
                    await Shell.Current.DisplayAlert("Alerta", "No existen usuarios con ese nombre", "OK");
                    return;
                }

                var decryptedPassword = DecryptPassword(usuarioCoincidente.Password);

 
                if (decryptedPassword == Password)
                {
                    bool isAdmin = usuarioCoincidente.IsAdmin;

                    var appShell = (AppShell)Shell.Current;

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
                Console.WriteLine($"Error: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Ocurrió un problema al intentar iniciar sesión", "OK");
            }
        }
    
        private string DecryptPassword(string cipherText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(_encryptionKey.PadRight(32));
                aes.IV = new byte[16]; // Vector de inicialización de 16 bytes
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var reader = new StreamReader(cs))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}