namespace CK3_Building_Merger
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
            btnSubmit = new Button();
            btnBrowse = new Button();
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textBox2 = new TextBox();
            button1 = new Button();
            label3 = new Label();
            textBox3 = new TextBox();
            button2 = new Button();
            btnClear = new Button();
            SuspendLayout();
            // 
            // btnSubmit
            // 
            btnSubmit.Location = new Point(77, 376);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(75, 23);
            btnSubmit.TabIndex = 0;
            btnSubmit.Text = "Build Mod";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += btnSubmit_Click;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(263, 103);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 2;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(69, 104);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(150, 23);
            textBox1.TabIndex = 3;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(69, 71);
            label1.Name = "label1";
            label1.Size = new Size(83, 15);
            label1.TabIndex = 8;
            label1.Text = "JSON File Path";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(69, 149);
            label2.Name = "label2";
            label2.Size = new Size(160, 15);
            label2.TabIndex = 11;
            label2.Text = "Steam Workshop Folder Path";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(69, 182);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(150, 23);
            textBox2.TabIndex = 10;
            // 
            // button1
            // 
            button1.Location = new Point(263, 181);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 9;
            button1.Text = "Browse";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(69, 230);
            label3.Name = "label3";
            label3.Size = new Size(119, 15);
            label3.TabIndex = 14;
            label3.Text = "CK3 Mod Folder Path";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(69, 261);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(150, 23);
            textBox3.TabIndex = 13;
            // 
            // button2
            // 
            button2.Location = new Point(263, 260);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 12;
            button2.Text = "Browse";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(171, 376);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 23);
            btnClear.TabIndex = 15;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(464, 450);
            Controls.Add(btnClear);
            Controls.Add(label3);
            Controls.Add(textBox3);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(textBox2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Controls.Add(btnBrowse);
            Controls.Add(btnSubmit);
            Name = "Form1";
            Text = "CK3 Buidling Merger";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSubmit;
        private Button btnBrowse;
        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private TextBox textBox2;
        private Button button1;
        private Label label3;
        private TextBox textBox3;
        private Button button2;
        private Button btnClear;
    }
}
