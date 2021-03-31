
namespace GUI
{
    partial class App
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.resultTab = new System.Windows.Forms.TabPage();
            this.matrixPanel = new System.Windows.Forms.TabControl();
            this.statusLabel = new System.Windows.Forms.Label();
            this.commandBox = new System.Windows.Forms.TextBox();
            this.matrixPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // resultTab
            // 
            this.resultTab.Location = new System.Drawing.Point(4, 24);
            this.resultTab.Name = "resultTab";
            this.resultTab.Padding = new System.Windows.Forms.Padding(3);
            this.resultTab.Size = new System.Drawing.Size(262, 187);
            this.resultTab.TabIndex = 0;
            this.resultTab.Text = "Результат";
            this.resultTab.UseVisualStyleBackColor = true;
            // 
            // matrixPanel
            // 
            this.matrixPanel.Controls.Add(this.resultTab);
            this.matrixPanel.ItemSize = new System.Drawing.Size(65, 20);
            this.matrixPanel.Location = new System.Drawing.Point(0, 0);
            this.matrixPanel.Name = "matrixPanel";
            this.matrixPanel.SelectedIndex = 0;
            this.matrixPanel.Size = new System.Drawing.Size(270, 215);
            this.matrixPanel.TabIndex = 99;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(4, 246);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(46, 15);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "Статус:";
            // 
            // commandBox
            // 
            this.commandBox.Location = new System.Drawing.Point(4, 220);
            this.commandBox.Name = "commandBox";
            this.commandBox.PlaceholderText = "Команда";
            this.commandBox.Size = new System.Drawing.Size(100, 23);
            this.commandBox.TabIndex = 2;
            this.commandBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.commandBox_KeyPress);
            this.commandBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.commandBox_PreviewKeyDown);
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(274, 267);
            this.Controls.Add(this.commandBox);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.matrixPanel);
            this.Name = "App";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Матричный калькулятор";
            this.Resize += new System.EventHandler(this.App_Resize);
            this.matrixPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage resultTab;
        private System.Windows.Forms.TabControl matrixPanel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.TextBox commandBox;
    }
}

