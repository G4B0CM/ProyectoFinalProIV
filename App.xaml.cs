using Avance2Progreso.Repositories;
namespace Avance2Progreso

{
    using Avance2Progreso.Services;
    using Avance2Progreso.Views.Admin;
    using Views;
    public partial class App : Application
    {
        public static UserRepository UserRepo { get; private set; }
        public static UserService UserService { get; private set; }
        public App(UserRepository repo, UserService us)
        {
            InitializeComponent();

            // Obtener el rol del usuario (puedes obtenerlo desde almacenamiento seguro, API, etc.)
            string userRole = GetUserRole();
            UserService = us;
            MainPage =new MainPage(UserService);
            UserRepo = repo;
        }

        private string GetUserRole()
        {
            return "Admin";
        }
    }
}
