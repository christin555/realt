using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RealtOn
{
    static class Program
    {
        enum Permissions
        {
            None,
            Guest,
            User,
            Admin
        }
        class Person
        {
            public const Permissions Permission = Permissions.None;
        }

        class Guest : Person
        {
            public new const Permissions Permission = Permissions.Guest;
        }
      
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new main());
        }
    }
}
