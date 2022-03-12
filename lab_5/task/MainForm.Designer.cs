namespace task
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.paintButton = new System.Windows.Forms.Button();
            this.taskSwitcher = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // paintButton
            // 
            this.paintButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.paintButton.Location = new System.Drawing.Point(227, 12);
            this.paintButton.Name = "paintButton";
            this.paintButton.Size = new System.Drawing.Size(130, 33);
            this.paintButton.TabIndex = 0;
            this.paintButton.Text = "Построить";
            this.paintButton.UseVisualStyleBackColor = true;
            this.paintButton.Click += new System.EventHandler(this.paintButton_Click);
            // 
            // taskSwitcher
            // 
            this.taskSwitcher.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.taskSwitcher.FormattingEnabled = true;
            this.taskSwitcher.Items.AddRange(new object[] { "Задание 1.1", "Задание 1.2", "Задание 1.3", "Задание 1.4", "Задание 1.5", "Задание 2", "Задание 3.1", "Задание 3.2" });
            this.taskSwitcher.Location = new System.Drawing.Point(12, 15);
            this.taskSwitcher.Name = "taskSwitcher";
            this.taskSwitcher.Size = new System.Drawing.Size(200, 28);
            this.taskSwitcher.TabIndex = 1;
            this.taskSwitcher.Text = "Выберите задание";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.taskSwitcher);
            this.Controls.Add(this.paintButton);
            this.Name = "MainForm";
            this.Text = "Рекурсия";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ComboBox taskSwitcher;

        private System.Windows.Forms.Button paintButton;

        #endregion
    }
}