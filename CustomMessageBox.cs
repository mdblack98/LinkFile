using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinkFile
{
    public partial class CustomMessageBox : Form
    {
        public CustomMessageBox()
        {
            InitializeComponent();
        }

        private void CustomMessageBox_Load(object sender, EventArgs e)
        {
            AutoSizeForm();
        }
        public CustomMessageBox(string message, string title)
        {
            InitializeComponent();
            this.Text = title;
            this.labelMessage.Text = message;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public DialogResult ShowCustomMessageBox(Form parent, string message, string title)
        {
            using (CustomMessageBox customMessageBox = new CustomMessageBox(message, title))
            {
                customMessageBox.StartPosition = FormStartPosition.Manual;
//                customMessageBox.Location = new Point(
//                    parent.Location.X + (parent.Width - customMessageBox.Width) / 2,
//                    parent.Location.Y + (parent.Height - customMessageBox.Height) / 2);
                customMessageBox.Location = new Point(
                    parent.Location.X + 30,
                    parent.Location.Y + 20);
                return customMessageBox.ShowDialog(parent);
            }
        }
        private void AutoSizeForm()
        {
            Size size = TextRenderer.MeasureText(this.labelMessage.Text, this.labelMessage.Font);
            this.labelMessage.Width = size.Width;
            this.labelMessage.Height = size.Height;

            // Adjust the form size to fit the label and buttons
            //this.ClientSize = new Size(this.labelMessage.Width + 40, this.labelMessage.Height + this.buttonYES.Height + 60);
            this.ClientSize = new Size(this.labelMessage.Width + 40, this.labelMessage.Height + 30);
            //this.Size = new Size(this.labelMessage.Width + 40, this.labelMessage.Height + 100);
            // Position the buttons
            //this.buttonYES.Location = new Point((this.ClientSize.Width - this.buttonYES.Width - this.buttonNO.Width) / 3, this.labelMessage.Bottom + 20);
            //this.buttonNO.Location = new Point(this.buttonYES.Right + this.buttonYES.Width / 2, this.labelMessage.Bottom + 20);
        }
    }
}
