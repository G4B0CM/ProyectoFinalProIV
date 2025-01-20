using Avance2Progreso.Interfaces;
using Avance2Progreso.Models;
using Avance2Progreso.Repositories;

namespace Avance2Progreso;

public partial class CreateConcursoPage : ContentPage
{
    IAdministradorRepository _administradorRepository;
    Competencias competencia = new Competencias();
    public CreateConcursoPage()
	{
        _administradorRepository = new AdministradorArchivosRepository();
        InitializeComponent();
        competencia = _administradorRepository.DevuelveCompetencia(); // Inicializamos un objeto vacío  
        BindingContext = competencia; // Se utiliza para vincular datos
    }
    
    private async void OnGuardarCompetenciaClicked(object sender, EventArgs e)
    {
        Competencias competencia = new Competencias
        {
            Id = Convert.ToInt32(editor_Id.Text),
            Nombre = editor_Nombre.Text,
            Categoria = editor_Categoria.Text,
            Descripcion = editor_Descripcion.Text,
        };

        string id = editor_Id.Text?.Trim();
        string nombre = editor_Nombre.Text?.Trim();
        string categoria = editor_Categoria.Text?.Trim(); 
        string descripcion = editor_Descripcion.Text?.Trim();

        // Validar que los campos no estén vacíos
        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(descripcion) || string.IsNullOrEmpty(categoria) || string.IsNullOrEmpty(id))
        {
            await DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
            return;
        }

        // Crear un nuevo objeto Competencia
        
        bool guardar_competencia = _administradorRepository.CrearCompetencia(competencia);
        
        // Asegúrate de tener una referencia a tu repositorio
        var repository = new Repositories.AdministradorArchivosRepository();
        if (guardar_competencia)
        {
            await DisplayAlert("Éxito", "Competencia creada correctamente.", "OK");
            Navigation.PushAsync(new CreateConcursoPage());
        }
        else
        {
            await DisplayAlert("Error", "No se pudo guardar la competencia.", "OK");
            Navigation.PushAsync(new CreateConcursoPage());
        }
    }
}
