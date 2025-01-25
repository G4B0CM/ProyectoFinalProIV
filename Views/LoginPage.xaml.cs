namespace Avance2Progreso.Views;


using Avance2Progreso.Models;
using Avance2Progreso.ViewModels;

public partial class LoginPage : ContentPage
{
    private int _indiceCarrusel = 0;
    public LoginPage()
    {
        InitializeComponent();
        BindingContext = new LoginPageViewModel();
        
    }

    
}