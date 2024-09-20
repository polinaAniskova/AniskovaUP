using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsUP
{
    public partial class Заявки : Form
    {
        private DataTable requestsTable;
        private int currentUserId;

        public Заявки(int userId)
        {
            InitializeComponent();
            this.currentUserId = userId;
            LoadRequests();
        }

        private void LoadRequests()
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            r.requestID,
                            c.fio AS [fio],   
                            c.phone AS [phone],  
                            r.startDate,
                            r.problemDescription,
                            r.model,
                            r.requestStatus,
                            r.masterID
                        FROM [dbo].[Requests] r
                        LEFT JOIN [dbo].[Users] c ON r.clientID = c.userID  
                        LEFT JOIN [dbo].[Users] m ON r.masterID = m.userID  
                        ";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    requestsTable = new DataTable();
                    adapter.Fill(requestsTable);

                    dataGridView1.DataSource = requestsTable;
                    SetupDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке заявок: " + ex.Message);
                }
            }
        }

        private void SetupDataGridView()
        {
            dataGridView1.Columns.Clear();

            DataGridViewComboBoxColumn statusColumn = new DataGridViewComboBoxColumn
            {
                Name = "requestStatus",
                HeaderText = "Статус заявки",
                DataSource = GetStatusData(),
                DisplayMember = "FullStatus", 
                ValueMember = "StatusId",
                DataPropertyName = "requestStatus"
            };
            dataGridView1.Columns.Add(statusColumn);

            DataGridViewComboBoxColumn masterColumn = new DataGridViewComboBoxColumn
            {
                Name = "masterID",
                HeaderText = "Мастер",
                DataSource = GetMasterData(),
                DisplayMember = "MasterInfo", 
                ValueMember = "userID",
                DataPropertyName = "masterID"
            };
            dataGridView1.Columns.Add(masterColumn);
        }

        private DataTable GetStatusData()
        {
            DataTable statusTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                connection.Open();
                string query = "SELECT StatusId, (StatusName + ' (' + CAST(StatusId AS VARCHAR) + ')') AS FullStatus FROM [dbo].[StatusRequest]";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(statusTable);
            }
            return statusTable;
        }

        private DataTable GetMasterData()
        {
            DataTable masterTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                connection.Open();
                string query = "SELECT userID, (fio + ' (' + phone + ')') AS MasterInfo FROM [dbo].[Users] WHERE [type] = 'Мастер'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(masterTable);
            }
            return masterTable;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                try
                {
                    connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand updateCommand = new SqlCommand(@"
                        UPDATE [dbo].[Requests]
                        SET
                            requestStatus = @requestStatus,
                            masterID = @masterID
                        WHERE requestID = @requestID", connection);

                    updateCommand.Parameters.Add("@requestStatus", SqlDbType.Int, 4, "requestStatus");
                    updateCommand.Parameters.Add("@masterID", SqlDbType.Int, 4, "masterID");
                    updateCommand.Parameters.Add("@requestID", SqlDbType.Int, 4, "requestID");

                    adapter.UpdateCommand = updateCommand;
                    adapter.Update(requestsTable);

                    MessageBox.Show("Изменения успешно сохранены!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении данных: " + ex.Message);
                }
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                LoadRequests();
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = @"
                    SELECT 
                        r.requestID,
                        c.fio AS [fio],   
                        c.phone AS [phone],  
                        r.startDate,
                        r.problemDescription,
                        r.model,
                        r.requestStatus,
                        r.masterID
                    FROM [dbo].[Requests] r
                    LEFT JOIN [dbo].[Users] c ON r.clientID = c.userID  
                    LEFT JOIN [dbo].[Users] m ON r.masterID = m.userID  
                    WHERE (m.fio LIKE @searchText OR c.fio LIKE @searchText OR r.requestStatus = @searchStatus)
                    ";

                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                        if (int.TryParse(searchText, out int status))
                        {
                            command.Parameters.AddWithValue("@searchStatus", status);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@searchStatus", DBNull.Value);
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        requestsTable = new DataTable();
                        adapter.Fill(requestsTable);

                        dataGridView1.DataSource = requestsTable;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при поиске: " + ex.Message);
                    }
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FormLog form = new FormLog();
            form.Show();
            this.Close();
        }
    }
}
