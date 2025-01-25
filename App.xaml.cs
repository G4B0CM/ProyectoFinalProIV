using Avance2Progreso.Repositories;
namespace Avance2Progreso

{
    using Views;
    public partial class App : Application
    {
        public static UserRepository UserRepo { get; private set; }
        public App(UserRepository repo)
        {
            InitializeComponent();

            // Obtener el rol del usuario (puedes obtenerlo desde almacenamiento seguro, API, etc.)
            string userRole = GetUserRole();

            MainPage =new Registro();
            UserRepo = repo;
        }

        private string GetUserRole()
        {
            return "Admin";
        }
    }
}
