namespace Avance2Progreso.Views;


using Avance2Progreso.Models;
using Avance2Progreso.ViewModels;

public partial class LoginPage : ContentPage
{
    private int _indiceCarrusel = 0;
    public LoginPage()
    {
        InitializeComponent();
        var ViewModel = new LoginPageViewModel();
        ViewModel.ShowAlert += async () =>
        {
            await DisplayAlert("Alerta", "Introduzca las credenciales correctamente", "Ok");
        };
    }

    private async void Registrarse_Clicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new Registro());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Excepción no controlada: {ex.Message}", "OK");
        }
    }
}