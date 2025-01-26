namespace Avance2Progreso.Views.Admin;

public partial class CompetenciasPage : ContentPage
{
	public CompetenciasPage()
	{
		InitializeComponent();
		BindingContext = new ViewModels.CompetenciasViewModel();
	}
}