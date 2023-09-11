namespace Package.Test
{
    partial class Form1
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
            opend = new Button();
            batch_import = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // opend
            // 
            opend.Location = new Point(48, 54);
            opend.Name = "opend";
            opend.Size = new Size(231, 99);
            opend.TabIndex = 0;
            opend.Text = "读取文件";
            opend.UseVisualStyleBackColor = true;
            opend.Click += opend_Click_1;
            // 
            // batch_import
            // 
            batch_import.Location = new Point(308, 54);
            batch_import.Name = "batch_import";
            batch_import.Size = new Size(163, 99);
            batch_import.TabIndex = 1;
            batch_import.Text = "批量导入";
            batch_import.UseVisualStyleBackColor = true;
            batch_import.Click += batch_import_Click;
            // 
            // button1
            // 
            button1.Location = new Point(308, 176);
            button1.Name = "button1";
            button1.Size = new Size(163, 99);
            button1.TabIndex = 2;
            button1.Text = "追加文件";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(batch_import);
            Controls.Add(opend);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button opend;
        private Button batch_import;
        private Button button1;
    }
}