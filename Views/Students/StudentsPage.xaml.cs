namespace Avance2Progreso.Views.Students;

public partial class StudentsPage : ContentPage
{
    public StudentsPage()
    {
        InitializeComponent();
        BindingContext= new ViewModels.AdministrarUsuariosViewModel();
    }
}