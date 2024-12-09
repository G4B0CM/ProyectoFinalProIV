using Avance2Progreso.Views;
using Avance2Progreso.Views.Admin;
using Avance2Progreso.Views.Students;

namespace Avance2Progreso
{
    public partial class AppShell : Shell
    {
        public AppShell(string userRole)
        {
            InitializeComponent();
            ConfigureTabs(userRole);
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
