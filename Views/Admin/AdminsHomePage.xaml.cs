using System.Windows.Input;

namespace Avance2Progreso.Views.Admin;

public partial class AdminsHomePage : ContentPage
{
    public AdminsHomePage()
    {
        InitializeComponent();
    }
    
    private async void NavigateToCreateConcurso(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CreateConcursoPage));

    }
    

}