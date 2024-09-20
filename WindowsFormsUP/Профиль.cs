using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsUP
{
    public partial class Профиль : Form
    {
        public Профиль(string userName, string userRole)
        {
            InitializeComponent();
            label1.Text = $"Добро пожаловать,\n{userName}.\nВаша роль: {userRole}.";
        }
    }
}
