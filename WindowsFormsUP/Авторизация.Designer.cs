namespace WindowsFormsUP
{
    partial class FormLog
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLog));
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.textBoxPass = new System.Windows.Forms.TextBox();
            this.textBoxCap = new System.Windows.Forms.TextBox();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.labelMistake = new System.Windows.Forms.Label();
            this.ButtonChange = new System.Windows.Forms.PictureBox();
            this.labelLockoutTime = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.buttonEYE = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ButtonChange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEYE)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Font = new System.Drawing.Font("Segoe UI Symbol", 27.75F, System.Drawing.FontStyle.Bold);
            this.textBoxLogin.ForeColor = System.Drawing.Color.Black;
            this.textBoxLogin.Location = new System.Drawing.Point(129, 82);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(258, 57);
            this.textBoxLogin.TabIndex = 2;
            // 
            // textBoxPass
            // 
            this.textBoxPass.Font = new System.Drawing.Font("Segoe UI Symbol", 27.75F, System.Drawing.FontStyle.Bold);
            this.textBoxPass.ForeColor = System.Drawing.Color.Black;
            this.textBoxPass.Location = new System.Drawing.Point(129, 145);
            this.textBoxPass.Name = "textBoxPass";
            this.textBoxPass.Size = new System.Drawing.Size(258, 57);
            this.textBoxPass.TabIndex = 3;
            // 
            // textBoxCap
            // 
            this.textBoxCap.Font = new System.Drawing.Font("Segoe UI Symbol", 27.75F, System.Drawing.FontStyle.Bold);
            this.textBoxCap.ForeColor = System.Drawing.Color.Black;
            this.textBoxCap.Location = new System.Drawing.Point(129, 357);
            this.textBoxCap.Name = "textBoxCap";
            this.textBoxCap.Size = new System.Drawing.Size(258, 57);
            this.textBoxCap.TabIndex = 6;
            this.textBoxCap.Visible = false;
            // 
            // buttonEnter
            // 
            this.buttonEnter.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonEnter.Font = new System.Drawing.Font("Segoe UI Symbol", 27.75F, System.Drawing.FontStyle.Bold);
            this.buttonEnter.ForeColor = System.Drawing.Color.Black;
            this.buttonEnter.Location = new System.Drawing.Point(129, 470);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(258, 76);
            this.buttonEnter.TabIndex = 5;
            this.buttonEnter.Text = "ВХОД";
            this.buttonEnter.UseVisualStyleBackColor = false;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // labelMistake
            // 
            this.labelMistake.AutoSize = true;
            this.labelMistake.Font = new System.Drawing.Font("Segoe UI Symbol", 27.75F, System.Drawing.FontStyle.Bold);
            this.labelMistake.ForeColor = System.Drawing.Color.Black;
            this.labelMistake.Location = new System.Drawing.Point(12, 418);
            this.labelMistake.Name = "labelMistake";
            this.labelMistake.Size = new System.Drawing.Size(0, 50);
            this.labelMistake.TabIndex = 5;
            this.labelMistake.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ButtonChange
            // 
            this.ButtonChange.Location = new System.Drawing.Point(129, 235);
            this.ButtonChange.Name = "ButtonChange";
            this.ButtonChange.Size = new System.Drawing.Size(258, 116);
            this.ButtonChange.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ButtonChange.TabIndex = 8;
            this.ButtonChange.TabStop = false;
            this.ButtonChange.Visible = false;
            this.ButtonChange.Click += new System.EventHandler(this.ButtonChange_Click);
            // 
            // labelLockoutTime
            // 
            this.labelLockoutTime.AutoSize = true;
            this.labelLockoutTime.Font = new System.Drawing.Font("Segoe UI Symbol", 27.75F, System.Drawing.FontStyle.Bold);
            this.labelLockoutTime.ForeColor = System.Drawing.Color.Black;
            this.labelLockoutTime.Location = new System.Drawing.Point(393, 483);
            this.labelLockoutTime.Name = "labelLockoutTime";
            this.labelLockoutTime.Size = new System.Drawing.Size(0, 50);
            this.labelLockoutTime.TabIndex = 12;
            this.labelLockoutTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(511, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(94, 72);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(83, 82);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 51);
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            // 
            // buttonEYE
            // 
            this.buttonEYE.Image = ((System.Drawing.Image)(resources.GetObject("buttonEYE.Image")));
            this.buttonEYE.Location = new System.Drawing.Point(83, 152);
            this.buttonEYE.Name = "buttonEYE";
            this.buttonEYE.Size = new System.Drawing.Size(40, 50);
            this.buttonEYE.TabIndex = 15;
            this.buttonEYE.TabStop = false;
            this.buttonEYE.Click += new System.EventHandler(this.buttonEYE_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Ebrima", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.Location = new System.Drawing.Point(129, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(258, 47);
            this.label2.TabIndex = 16;
            this.label2.Text = "Авторизация";
            // 
            // FormLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(617, 561);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonEYE);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelLockoutTime);
            this.Controls.Add(this.ButtonChange);
            this.Controls.Add(this.labelMistake);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.textBoxCap);
            this.Controls.Add(this.textBoxPass);
            this.Controls.Add(this.textBoxLogin);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация";
            ((System.ComponentModel.ISupportInitialize)(this.ButtonChange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEYE)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.TextBox textBoxPass;
        private System.Windows.Forms.TextBox textBoxCap;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.Label labelMistake;
        private System.Windows.Forms.PictureBox ButtonChange;
        private System.Windows.Forms.Label labelLockoutTime;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox buttonEYE;
        private System.Windows.Forms.Label label2;
    }
}

