
namespace Avance2Progreso

{
    using Views;
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Obtener el rol del usuario (puedes obtenerlo desde almacenamiento seguro, API, etc.)
            string userRole = GetUserRole();

            // Inicializar AppShell con el rol
            MainPage = new LoginPage();
        }

        private string GetUserRole()
        {
            // Lógica para obtener el rol del usuario (Admin, Student, etc.)
            // Por ejemplo, "Admin", "Student", o "" si no está autenticado.
            return "Admin"; // Ejemplo estático, reemplazar con lógica real.
        }
    }
}
