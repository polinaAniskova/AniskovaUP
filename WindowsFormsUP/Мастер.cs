using System;
using System.Windows.Forms;
using static ClassLibraryPolya.ClientManager;

namespace WindowsFormsUP
{
    public partial class Мастер : Form
    {
        private int currentUserId;
        private MasterManager masterManager;

        public Мастер(int userId)
        {
            InitializeComponent();
            this.currentUserId = userId;
            masterManager = new MasterManager();
            LoadMasterRequests();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void LoadMasterRequests()
        {
            try
            {
                var dataTable = masterManager.GetMasterRequests(currentUserId);
                dataGridView1.DataSource = dataTable;

                dataGridView1.Columns["requestID"].HeaderText = "ID заявки";
                dataGridView1.Columns["startDate"].HeaderText = "Дата начала";
                dataGridView1.Columns["problemDescription"].HeaderText = "Описание проблемы";
                dataGridView1.Columns["requestStatus"].HeaderText = "Статус заявки";
                dataGridView1.Columns["repairParts"].HeaderText = "Запчасти";
                dataGridView1.Columns["model"].HeaderText = "Модель";
                dataGridView1.Columns["techTypeID"].HeaderText = "ID типа техники";
                dataGridView1.Columns["techTypeName"].HeaderText = "Тип техники";
                dataGridView1.Columns["message"].HeaderText = "Комментарий";

                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке заявок: " + ex.Message);
            }
        }

        private void ConfigureDataGridView()
        {
            dataGridView1.Columns["requestStatus"].ReadOnly = false;
            dataGridView1.Columns["repairParts"].ReadOnly = false;
            dataGridView1.Columns["message"].ReadOnly = false;

            dataGridView1.Columns["requestID"].ReadOnly = true;
            dataGridView1.Columns["startDate"].ReadOnly = true;
            dataGridView1.Columns["problemDescription"].ReadOnly = true;
            dataGridView1.Columns["model"].ReadOnly = true;
            dataGridView1.Columns["techTypeID"].ReadOnly = true;
            dataGridView1.Columns["techTypeName"].ReadOnly = true;
        }

        private void SaveChanges()
        {
            try
            {
                masterManager.SaveMasterChanges(dataGridView1);
                MessageBox.Show("Изменения успешно сохранены.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении изменений: " + ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormLog formLog = new FormLog();
            formLog.Show();
            this.Close();
        }
    }
}
