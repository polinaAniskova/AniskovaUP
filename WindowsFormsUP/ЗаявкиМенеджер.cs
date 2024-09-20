using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsUP
{
    public partial class ЗаявкиМенеджер : Form
    {
        public ЗаявкиМенеджер()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                // Открываем соединение
                connection.Open();

                // SQL-запрос для получения данных
                string query = @"SELECT 
                                    R.requestID,
                                    R.startDate,
                                    R.problemDescription,
                                    S.StatusName,
                                    R.completionDate,
                                    R.repairParts,
                                    R.masterID,
                                    U.fio,
                                    R.model,
                                    T.techTypeName,
                                    C.message
                                FROM 
                                    Requests R
                                LEFT JOIN 
                                    Users U ON R.masterID = U.userID
                                LEFT JOIN 
                                    StatusRequest S ON R.requestStatus = S.StatusId
                                LEFT JOIN 
                                    TechTypes T ON R.techTypeID = T.techTypeID
                                LEFT JOIN 
                                    Comments C ON R.requestID = C.requestID";

                // Создание DataAdapter для заполнения DataGridView
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();

                // Заполняем DataTable
                dataAdapter.Fill(dataTable);

                // Привязываем DataTable к DataGridView
                dataGridView1.DataSource = dataTable;

                // Закрываем соединение
                connection.Close();
            }
        }
    }
}
