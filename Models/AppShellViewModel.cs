using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avance2Progreso.Models
{
    public class AppShellViewModel
    {
        public bool IsAdminRole { get; }
        public bool IsStudentRole { get; }
        public bool IsDefaultRole { get; }

        public AppShellViewModel(string userRole)
        {
            IsAdminRole = userRole == "Admin";
            IsStudentRole = userRole == "Student";
            IsDefaultRole = string.IsNullOrEmpty(userRole);
        }
    }
}
