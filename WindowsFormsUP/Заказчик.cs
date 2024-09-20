using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ClassLibraryPolya; // Добавьте ссылку на библиотеку классов

namespace WindowsFormsUP
{
    public partial class Заказчик : Form
    {
        private int currentUserId;
        private readonly ClientManager _clientManager;

        public Заказчик(int userId)
        {
            InitializeComponent();
            this.currentUserId = userId;
            _clientManager = new ClientManager();
        }

        private void FormUser_Load(object sender, EventArgs e)
        {
            FillTechTypeComboBox();
        }

        private void FillTechTypeComboBox()
        {
            var techTypes = _clientManager.GetTechTypes();
            comboBoxType.Items.Clear();

            foreach (var techType in techTypes)
            {
                comboBoxType.Items.Add(techType);
            }

            comboBoxType.Refresh();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            buttonСreate.BackColor = Color.SteelBlue;
            buttonAll.BackColor = Color.DodgerBlue;
        }

        private void buttonAll_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            buttonСreate.BackColor = Color.DodgerBlue;
            buttonAll.BackColor = Color.SteelBlue;
            LoadClientRequests();
        }

        private void LoadClientRequests()
        {
            var dataTable = _clientManager.GetClientRequests(currentUserId);
            dataGridView1.DataSource = dataTable;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            FormLog formLog = new FormLog();
            formLog.Show();
            this.Close();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string techType = comboBoxType.Text;
            string model = textBoxModel.Text;
            string problemDescription = textBoxProblem.Text;

            if (string.IsNullOrEmpty(techType) || string.IsNullOrEmpty(model) || string.IsNullOrEmpty(problemDescription))
            {
                MessageBox.Show("Пожалуйста, заполните все поля!");
                return;
            }

            DialogResult result = MessageBox.Show("Отправить заявку?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            bool success = _clientManager.CreateRequest(currentUserId, techType, model, problemDescription);

            if (success)
            {
                MessageBox.Show("Заявка успешно создана!");
            }
        }
    }
}
