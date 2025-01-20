using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System;
using System.Windows.Input;

namespace Avance2Progreso.ViewModels
{
    public class LoginPageViewModel : ObservableObject
    {
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand NavigateToRegistroCommand { get; }

        public LoginPageViewModel()
        {
            NavigateToRegistroCommand = new RelayCommand(OnNavigateToRegistro);
        }

        private async void OnNavigateToRegistro()
        {
            try
            {
                await Shell.Current.GoToAsync("//registro");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}