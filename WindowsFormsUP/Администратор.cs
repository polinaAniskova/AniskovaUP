using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using ClassLibraryPolya; // Добавьте ссылку на библиотеку классов

namespace WindowsFormsUP
{
    public partial class Администратор : Form
    {
        private DataTable loginHistoryTable;
        private readonly AdminManager _adminManager;

        public Администратор()
        {
            InitializeComponent();
            _adminManager = new AdminManager();
            LoadLoginHistory();
        }

        public void GenerateQRCode(string url)
        {
            _adminManager.GenerateQRCode(url, pictureBoxQRCode);
        }

        private void LoadLoginHistory()
        {
            loginHistoryTable = _adminManager.LoadLoginHistory();
            dataGridView1.DataSource = loginHistoryTable;
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (loginHistoryTable == null)
            {
                return;
            }

            string filterExpression = string.Empty;
            string searchText = textBoxSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                dataGridView1.DataSource = loginHistoryTable;
            }
            else
            {
                if (int.TryParse(searchText, out int numericValue))
                {
                    filterExpression = $"[Вход подтверждён] = {numericValue}";
                }
                else
                {
                    filterExpression = $"[Логин пользователя] LIKE '%{searchText}%'";
                }

                DataView dataView = new DataView(loginHistoryTable);
                dataView.RowFilter = filterExpression;
                dataGridView1.DataSource = dataView;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormLog formLog = new FormLog();
            formLog.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string feedbackUrl = "https://forms.gle/945x9XrTbMzCrheA7";
            GenerateQRCode(feedbackUrl);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ЗаявкиМенеджер formman = new ЗаявкиМенеджер();
            formman.ShowDialog();
        }
    }
}
