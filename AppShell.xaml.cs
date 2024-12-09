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
            // Limpiar Tabs existentes
            Items.Clear();

            if (role == "Admin")
            {
                // Tabs para Admin
                Items.Add(CreateShellContent("Inicio", "admin_home_icon.png", typeof(AdminsHomePage)));
                Items.Add(CreateShellContent("Admins", "admins_icon.png", typeof(Admins)));
                Items.Add(CreateShellContent("Estudiantes", "students_icon.png", typeof(StudentsPage)));
            }
            else if (role == "Student")
            {
                // Tabs para Student
                Items.Add(CreateShellContent("Inicio", "student_home_icon.png", typeof(StudentsHomePage)));
            }
            else
            {
                // Tabs para usuario no autenticado
                Items.Add(CreateShellContent("Iniciar sesión", "login_icon.png", typeof(LoginPage)));
            }
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
