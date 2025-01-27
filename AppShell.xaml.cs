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
            Routing.RegisterRoute("registro", typeof(Registro));
            Routing.RegisterRoute("AdminsHomePage", typeof(AdminsHomePage));
            Routing.RegisterRoute("StudentsHomePage", typeof(StudentsHomePage));
            AdminTabs.IsVisible = false;
            StudentTabs.IsVisible = false;

        }

        public void SetUserTabs(bool isAdmin)
        {

            if (isAdmin)
            {
                AdminTabs.IsVisible = true;
                StudentTabs.IsVisible = false;
            }
            else
            {
                AdminTabs.IsVisible = false;
                StudentTabs.IsVisible = true;
            }
        }
    
    }
}
