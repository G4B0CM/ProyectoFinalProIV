using Avance2Progreso.Views;
using Avance2Progreso.Views.Admin;
using Avance2Progreso.Views.Students;

namespace Avance2Progreso
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            ConfigureTabs("Admin");
            Routing.RegisterRoute(nameof(CreateConcursoPage), typeof(CreateConcursoPage));
            Routing.RegisterRoute("registro", typeof(Registro));
            Routing.RegisterRoute("Admins", typeof(Admins));
            Routing.RegisterRoute("StudentsPage", typeof(StudentsPage));

        }

        private void ConfigureTabs(string role)
        {
            // Configurar visibilidad según el rol
            AdminInicio.IsVisible = role == "Admin";
            AdminAdmins.IsVisible = role == "Admin";
            AdminEstudiantes.IsVisible = role == "Admin";

            StudentInicio.IsVisible = role == "Student";

            DefaultLogin.IsVisible = role == "Default";
        }

        private ShellContent CreateShellContent(string title, string icon, Type pageType)
        {
            return new ShellContent
            {
                Title = title,
                Icon = icon,
                ContentTemplate = new DataTemplate(pageType)
            };
        }
    }
}
