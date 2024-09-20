using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ClassLibraryPolya;
using static ClassLibraryPolya.ClientManager;

namespace UnitTestPolya
{
    [TestClass]
    public class ClientManagerTests
    {
        private ClientManager clientManager;

        [TestInitialize]
        public void SetUp()
        {
            // Создаем новый экземпляр ClientManager перед каждым тестом
            clientManager = new ClientManager();
        }

        [TestMethod]
        public void TestGetTechTypes_ReturnsEmptyListOnNoData()
        {
            // Arrange: Очистка таблицы TechTypes перед тестом
            ClearTechTypesTable();

            // Act: Выполняем метод для получения типов техники
            var result = clientManager.GetTechTypes();

            // Assert: Проверяем, что возвращается пустой список, если нет данных
            Assert.AreEqual(0, result.Count, "Ожидался пустой список, когда нет типов техники.");
        }

        [TestMethod]
        public void TestCreateRequest_ReturnsTrueOnSuccess()
        {
            // Arrange: Убедитесь, что тип техники существует для успешного создания заявки
            var techTypeName = "Фен";
            AddTechType(techTypeName);
            var clientId = 1;
            var model = "1";
            var problemDescription = "Problem";

            // Act: Попытка создать заявку с существующим типом техники
            var result = clientManager.CreateRequest(clientId, techTypeName, model, problemDescription);

            // Assert: Проверяем, что метод возвращает true при успешном создании заявки
            Assert.IsTrue(result, "Ожидалось, что CreateRequest вернет true при успешном создании заявки.");

            // Clean-up: Удаляем созданную заявку (если нужно) и тип техники
            RemoveTechType(techTypeName);
        }

        private void ClearTechTypesTable()
        {
            // Удаляем все записи из таблицы TechTypes
            using (var connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM TechTypes", connection);
                command.ExecuteNonQuery();
            }
        }

        private void AddTechType(string techTypeName)
        {
            // Добавляем тип техники в таблицу TechTypes
            using (var connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO TechTypes (techTypeName) VALUES (@techTypeName)", connection);
                command.Parameters.AddWithValue("@techTypeName", techTypeName);
                command.ExecuteNonQuery();
            }
        }

        private void RemoveTechType(string techTypeName)
        {
            // Удаляем тип техники из таблицы TechTypes
            using (var connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM TechTypes WHERE techTypeName = @techTypeName", connection);
                command.Parameters.AddWithValue("@techTypeName", techTypeName);
                command.ExecuteNonQuery();
            }
        }
    }

    [TestClass]
    public class MasterManagerTests
    {
        private MasterManager masterManager;

        [TestInitialize]
        public void SetUp()
        {
            // Создаем новый экземпляр MasterManager перед каждым тестом
            masterManager = new MasterManager();
        }

        [TestMethod]
        public void TestGetMasterRequests_ReturnsEmptyDataTableOnNoData()
        {
            // Arrange: Убедитесь, что в таблице Requests нет данных для теста
            ClearRequestsTable();

            // Act: Выполняем метод для получения заявок мастера
            var result = masterManager.GetMasterRequests(1);

            // Assert: Проверяем, что возвращается пустой DataTable, если нет данных
            Assert.AreEqual(0, result.Rows.Count, "Ожидался пустой DataTable, когда нет заявок.");
        }

        [TestMethod]
        public void TestSaveMasterChanges_HandlesEmptyDataGridView()
        {
            // Arrange: Создаем DataGridView без строк
            var dataGridView = new DataGridView();
            dataGridView.Columns.Add("requestID", "Request ID"); // Добавляем необходимую колонку
            dataGridView.Columns.Add("requestStatus", "Request Status"); // Добавляем необходимую колонку

            // Act: Сохраняем изменения мастера для пустого DataGridView
            try
            {
                masterManager.SaveMasterChanges(dataGridView);
                // Assert: Никакое исключение не должно быть выброшено для пустого DataGridView
                Assert.IsTrue(true, "Ожидалось, что SaveMasterChanges обработает пустой DataGridView без ошибок.");
            }
            catch (Exception ex)
            {
                Assert.Fail("Ожидалось отсутствие исключения, но было выброшено: " + ex.Message);
            }
        }

        [TestMethod]
        public void TestSaveMasterChanges_HandlesDataGridViewWithRows()
        {
            // Arrange: Создаем DataGridView с одной строкой и необходимыми столбцами
            var dataGridView = new DataGridView();
            dataGridView.Columns.Add("requestID", "Request ID");
            dataGridView.Columns.Add("requestStatus", "Request Status");
            dataGridView.Columns.Add("repairParts", "Repair Parts");
            dataGridView.Columns.Add("message", "Message");
            dataGridView.Rows.Add(1, 1, "Parts", "Comment");

            // Act: Сохраняем изменения мастера для заполненного DataGridView
            try
            {
                masterManager.SaveMasterChanges(dataGridView);
                // Assert: Никакое исключение не должно быть выброшено для заполненного DataGridView
                Assert.IsTrue(true, "Ожидалось, что SaveMasterChanges обработает заполненный DataGridView без ошибок.");
            }
            catch (Exception ex)
            {
                Assert.Fail("Ожидалось отсутствие исключения, но было выброшено: " + ex.Message);
            }
        }

        private void ClearRequestsTable()
        {
            // Удаляем все записи из таблицы Requests
            using (var connection = new SqlConnection(DbConnectionHelper.ConnectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Requests", connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
