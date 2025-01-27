using Avance2Progreso.Repositories;
namespace Avance2Progreso

{
    using Avance2Progreso.Views.Admin;
    using Views;
    public partial class App : Application
    {
        public static UserRepository UserRepo { get; private set; }
        public App(UserRepository repo)
        {
            InitializeComponent();


            MainPage =new LoginPage();
            UserRepo = repo;
        }

    }
}
