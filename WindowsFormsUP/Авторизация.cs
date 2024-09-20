using System;
using System.Drawing;
using System.Windows.Forms;
using ClassLibraryPolya;

namespace WindowsFormsUP
{
    public partial class FormLog : Form
    {
        private AuthManager authManager;
        private string connectionString = "your_connection_string_here"; // Обновите строку подключения

        public FormLog()
        {
            InitializeComponent();
            textBoxPass.UseSystemPasswordChar = true;
            ButtonChange.Click += ButtonChange_Click;
            buttonEnter.Click += buttonEnter_Click;
            authManager = new AuthManager();
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if (authManager.IsLockedOut)
            {
                ShowLockoutMessage();
                return;
            }

            if (authManager.CaptchaVisible && !authManager.CaptchaVerified)
            {
                HandleCaptchaVerification();
                return;
            }

            HandleLogin();
        }

        private void HandleCaptchaVerification()
        {
            if (textBoxCap.Text != authManager.CaptchaText)
            {
                authManager.CaptchaAttempts++;
                if (authManager.CaptchaAttempts >= 3)
                {
                    authManager.InitiateLockout();
                }
                else
                {
                    labelMistake.Text = "Неверная капча!";
                    labelMistake.ForeColor = Color.Red;
                    authManager.GenerateCaptcha(ButtonChange);
                    ButtonChange.Visible = true;
                }
            }
            else
            {
                authManager.CaptchaVerified = true;
                if (!authManager.ValidateUser(textBoxLogin.Text, textBoxPass.Text, connectionString, out int userId))
                {
                    HandleLoginFailure();
                }
                else
                {
                    ProceedToUserForm(userId);
                }
            }
        }

        private void HandleLogin()
        {
            if (authManager.ValidateUser(textBoxLogin.Text, textBoxPass.Text, connectionString, out int userId))
            {
                ProceedToUserForm(userId);
            }
            else
            {
                HandleLoginFailure();
            }
        }

        private void HandleLoginFailure()
        {
            if (!authManager.CaptchaVisible)
            {
                authManager.GenerateCaptcha(ButtonChange);
                authManager.CaptchaVisible = true;
                textBoxCap.Visible = true;
                ButtonChange.Visible = true;
                labelMistake.Text = "Неверный логин или пароль!";
                labelMistake.ForeColor = Color.Red;

                authManager.SaveLoginHistory(textBoxLogin.Text, false, connectionString);

                authManager.FailedAttempts++;
            }
            else
            {
                authManager.InitiateLockout();
            }
        }

        private void ProceedToUserForm(int userId)
        {
            authManager.SaveLoginHistory(textBoxLogin.Text, true, connectionString);
            // Open the appropriate user form based on userId and role
        }

        private void ButtonChange_Click(object sender, EventArgs e)
        {
            authManager.GenerateCaptcha(ButtonChange);
            textBoxCap.Text = "";
        }

        private void ShowLockoutMessage()
        {
            if (authManager.IsLockedOut)
            {
                MessageBox.Show("Вход заблокирован на 3 минуты из-за слишком большого количества попыток.");
            }
        }

        private void buttonEYE_Click(object sender, EventArgs e)
        {
            authManager.IsPasswordVisible = !authManager.IsPasswordVisible;
            textBoxPass.UseSystemPasswordChar = !authManager.IsPasswordVisible;
        }
    }
}
