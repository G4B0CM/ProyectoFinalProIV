namespace Avance2Progreso.Views;

public partial class Registro : ContentPage
{
	public Registro()
	{
		InitializeComponent();
        BindingContext = new ViewModels.UserViewModel();
    }
}