namespace Avance2Progreso.Views;
using Avance2Progreso.Models;
public partial class LoginPage : ContentPage
{
    private int _indiceCarrusel = 0;
    public LoginPage()
    {
        InitializeComponent();
        
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