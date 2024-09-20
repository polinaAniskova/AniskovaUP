using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ClassLibraryPolya
{
    public static class DbConnectionHelper
    {
        public static string ConnectionString { get; } = @"Data Source=DESKTOP-LFEJRK0;Initial Catalog=МокшановУП;Integrated Security=True";
    }

    public class ClientManager
    {
        public List<string> GetTechTypes()
        {
            List<string> techTypes = new List<string>();

            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT techTypeName FROM TechTypes";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            techTypes.Add(reader["techTypeName"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке типов техники: " + ex.Message);
                }
            }

            return techTypes;
        }

        public DataTable GetClientRequests(int clientId)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = @"
                        SELECT 
                            r.startDate AS [Дата заявки],
                            r.problemDescription AS [Описание проблемы],
                            t.techTypeName AS [Тип техники],
                            r.model AS [Модель техники],
                            s.StatusName AS [Статус заявки],
                            r.completionDate AS [Дата завершения]
                        FROM [dbo].[Requests] r
                        JOIN [dbo].[TechTypes] t ON r.techTypeID = t.techTypeID
                        JOIN [dbo].[StatusRequest] s ON r.requestStatus = s.StatusId
                        WHERE r.clientID = @clientId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@clientId", clientId);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dataTable);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке заявок: " + ex.Message);
                }
            }

            return dataTable;
        }

        public bool CreateRequest(int clientId, string techTypeName, string model, string problemDescription)
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                try
                {
                    connection.Open();

                    string getTechTypeIdQuery = "SELECT techTypeID FROM TechTypes WHERE techTypeName = @techTypeName";
                    int techTypeId;

                    using (SqlCommand getTechTypeIdCommand = new SqlCommand(getTechTypeIdQuery, connection))
                    {
                        getTechTypeIdCommand.Parameters.AddWithValue("@techTypeName", techTypeName);
                        var resultTechType = getTechTypeIdCommand.ExecuteScalar();
                        if (resultTechType != null)
                        {
                            techTypeId = Convert.ToInt32(resultTechType);
                        }
                        else
                        {
                            string insertTechTypeQuery = "INSERT INTO TechTypes (techTypeName) OUTPUT INSERTED.techTypeID VALUES (@techTypeName)";
                            using (SqlCommand insertTechTypeCommand = new SqlCommand(insertTechTypeQuery, connection))
                            {
                                insertTechTypeCommand.Parameters.AddWithValue("@techTypeName", techTypeName);
                                techTypeId = (int)insertTechTypeCommand.ExecuteScalar();
                            }
                        }
                    }

                    int requestId = GetNextRequestID();
                    if (requestId == -1)
                    {
                        return false;
                    }

                    string insertRequestQuery = @"
                INSERT INTO Requests 
                    (requestID, startDate, problemDescription, requestStatus, completionDate, repairParts, masterID, clientID, model, techTypeID)
                VALUES 
                    (@requestID, @startDate, @problemDescription, @requestStatus, @completionDate, @repairParts, @masterID, @clientID, @model, @techTypeID)";

                    using (SqlCommand insertRequestCommand = new SqlCommand(insertRequestQuery, connection))
                    {
                        insertRequestCommand.Parameters.AddWithValue("@requestID", requestId);
                        insertRequestCommand.Parameters.AddWithValue("@startDate", DateTime.Now);
                        insertRequestCommand.Parameters.AddWithValue("@problemDescription", problemDescription);
                        insertRequestCommand.Parameters.AddWithValue("@requestStatus", 1);
                        insertRequestCommand.Parameters.AddWithValue("@completionDate", DBNull.Value);
                        insertRequestCommand.Parameters.AddWithValue("@repairParts", DBNull.Value);
                        insertRequestCommand.Parameters.AddWithValue("@masterID", DBNull.Value);
                        insertRequestCommand.Parameters.AddWithValue("@clientID", clientId);
                        insertRequestCommand.Parameters.AddWithValue("@model", model);
                        insertRequestCommand.Parameters.AddWithValue("@techTypeID", techTypeId);
                        insertRequestCommand.ExecuteNonQuery();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при создании заявки: " + ex.Message);
                    return false;
                }
            }
        }

        private int GetNextRequestID()
        {
            using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT ISNULL(MAX(requestID), 0) + 1 FROM Requests";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при получении следующего ID заявки: " + ex.Message);
                    return -1;
                }
            }
        }

        public class MasterManager
        {
            public DataTable GetMasterRequests(int masterId)
            {
                DataTable dataTable = new DataTable();

                using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = @"
                        SELECT r.requestID, r.startDate, r.problemDescription, r.requestStatus, 
                               r.repairParts, r.model, r.techTypeID, t.techTypeName, c.message
                        FROM Requests r
                        LEFT JOIN TechTypes t ON r.techTypeID = t.techTypeID
                        LEFT JOIN Comments c ON r.requestID = c.requestID
                        WHERE r.masterID = @masterID";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@masterID", masterId);

                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                adapter.Fill(dataTable);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при загрузке заявок: " + ex.Message);
                    }
                }

                return dataTable;
            }

            public void SaveMasterChanges(DataGridView dataGridView)
            {
                using (SqlConnection connection = new SqlConnection(DbConnectionHelper.ConnectionString))
                {
                    try
                    {
                        connection.Open();

                        foreach (DataGridViewRow row in dataGridView.Rows)
                        {
                            if (row.IsNewRow) continue;

                            int requestId = Convert.ToInt32(row.Cells["requestID"].Value);
                            int requestStatus = Convert.ToInt32(row.Cells["requestStatus"].Value);
                            string repairParts = row.Cells["repairParts"].Value?.ToString();
                            string message = row.Cells["message"].Value?.ToString();
                            DateTime? completionDate = null;

                            if (requestStatus == 3)
                            {
                                completionDate = DateTime.Now;
                            }

                            string updateRequestQuery = @"
                            UPDATE Requests
                            SET requestStatus = @requestStatus, repairParts = @repairParts, completionDate = @completionDate
                            WHERE requestID = @requestID";

                            using (SqlCommand updateRequestCommand = new SqlCommand(updateRequestQuery, connection))
                            {
                                updateRequestCommand.Parameters.AddWithValue("@requestStatus", requestStatus);
                                updateRequestCommand.Parameters.AddWithValue("@repairParts", (object)repairParts ?? DBNull.Value);
                                updateRequestCommand.Parameters.AddWithValue("@completionDate", (object)completionDate ?? DBNull.Value);
                                updateRequestCommand.Parameters.AddWithValue("@requestID", requestId);

                                updateRequestCommand.ExecuteNonQuery();
                            }

                            string updateCommentQuery = @"
                            UPDATE Comments
                            SET message = @message
                            WHERE requestID = @requestID";

                            using (SqlCommand updateCommentCommand = new SqlCommand(updateCommentQuery, connection))
                            {
                                updateCommentCommand.Parameters.AddWithValue("@message", (object)message ?? DBNull.Value);
                                updateCommentCommand.Parameters.AddWithValue("@requestID", requestId);

                                updateCommentCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при сохранении изменений: " + ex.Message);
                    }
                }
            }
        }
    }
}