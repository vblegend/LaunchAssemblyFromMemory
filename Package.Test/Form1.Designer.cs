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
            this.opend = new System.Windows.Forms.Button();
            this.batch_import = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // opend
            // 
            this.opend.Location = new System.Drawing.Point(48, 54);
            this.opend.Name = "opend";
            this.opend.Size = new System.Drawing.Size(231, 99);
            this.opend.TabIndex = 0;
            this.opend.Text = "读取文件";
            this.opend.UseVisualStyleBackColor = true;
            this.opend.Click += new System.EventHandler(this.opend_Click_2);
            // 
            // batch_import
            // 
            this.batch_import.Location = new System.Drawing.Point(308, 54);
            this.batch_import.Name = "batch_import";
            this.batch_import.Size = new System.Drawing.Size(163, 99);
            this.batch_import.TabIndex = 1;
            this.batch_import.Text = "批量导入";
            this.batch_import.UseVisualStyleBackColor = true;
            this.batch_import.Click += new System.EventHandler(this.batch_import_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(308, 176);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 99);
            this.button1.TabIndex = 2;
            this.button1.Text = "追加文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(48, 176);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(163, 99);
            this.button2.TabIndex = 3;
            this.button2.Text = "替换资源";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(658, 282);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(116, 66);
            this.button3.TabIndex = 4;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.batch_import);
            this.Controls.Add(this.opend);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Button opend;
        private Button batch_import;
        private Button button1;
        private Button button2;
        private Button button3;
    }
}