namespace LinkFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            buttonFile1 = new Button();
            buttonFile2 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            richTextBox1 = new RichTextBox();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonFile1
            // 
            buttonFile1.Location = new Point(17, 21);
            buttonFile1.Name = "buttonFile1";
            buttonFile1.Size = new Size(75, 23);
            buttonFile1.TabIndex = 0;
            buttonFile1.Text = "File#1";
            buttonFile1.UseVisualStyleBackColor = true;
            buttonFile1.Click += buttonFile1_Click;
            // 
            // buttonFile2
            // 
            buttonFile2.Location = new Point(17, 50);
            buttonFile2.Name = "buttonFile2";
            buttonFile2.Size = new Size(75, 23);
            buttonFile2.TabIndex = 1;
            buttonFile2.Text = "File#2";
            buttonFile2.UseVisualStyleBackColor = true;
            buttonFile2.Click += buttonFile2_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(109, 22);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(356, 23);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += textBox1_TextChanged;
            textBox1.MouseLeave += textBox1_MouseLeave;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(109, 50);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(356, 23);
            textBox2.TabIndex = 3;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(19, 79);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(436, 95);
            richTextBox1.TabIndex = 4;
            richTextBox1.Text = "Combines File1 and File2 and creates a hard link.\nCan autodetect WSJT-X ADIF file for combining.\nSee tooltip on File#2 for options\nhttps://github.com/mdblack98/LinkFile\n";
            richTextBox1.LinkClicked += richTextBox1_LinkClicked;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 186);
            Controls.Add(richTextBox1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(buttonFile2);
            Controls.Add(buttonFile1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "LinkFile 20240531";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private Button buttonFile1;
        private Button buttonFile2;
        private TextBox textBox1;
        private TextBox textBox2;
        private RichTextBox richTextBox1;
    }
}
