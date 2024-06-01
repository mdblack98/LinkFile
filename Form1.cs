using System.Diagnostics;
using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace LinkFile
{
    public partial class Form1 : Form
    {
        CustomMessageBox myBox = new CustomMessageBox();
        public Form1()
        {
            InitializeComponent();
            richTextBox1.AutoScrollOffset = new Point(0, 0);
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(buttonFile1, "Select the first file");
            toolTip1.SetToolTip(buttonFile2, "Select the second file\nCtrl-Click to combine with File#1\nShift-click to create a new hardlink");
        }

        private string getFileName(string path, bool exists = true)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "All files | *.*";
            openFileDialog1.Title = "Select a file";
            openFileDialog1.Multiselect = false;
            openFileDialog1.CheckFileExists = exists;
            if (path is not null)
                openFileDialog1.InitialDirectory = path;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog1.FileName;
            }
            return "";
        }

        public static string ComputeMD5Checksum(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return "";
            }
            try
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(filePath))
                    {
                        byte[] hash = md5.ComputeHash(stream);
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < hash.Length; i++)
                        {
                            sb.Append(hash[i].ToString("x2"));
                        }
                        return sb.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ComputeMD5Checksum error: " + ex.Message + "\nFilename: " + filePath);
            }
            return "";
        }
        private bool isADIF(string path)
        {

            if (path is not null)
            {
                if (path.EndsWith(".adi"))
                    return true;
            }
            return false;
        }
        private void buttonFile1_Click(object sender, EventArgs e)
        {
            textBox1.Text = getFileName(openFileDialog1.FileName);
            if (textBox1.Text.Length == 0) return;
            string md5sum1 = ComputeMD5Checksum(textBox1.Text);
            string md5sum2 = "";
            if (textBox2.Text.Length > 0) md5sum2 = ComputeMD5Checksum(textBox2.Text);
            if (md5sum1.Equals(md5sum2))
            {
                MessageBox.Show("Files are the same!!  Already linked?");
            }

            if (isADIF(textBox1.Text))
            {
                richTextBox1.AppendText("ADIF detected in " + textBox1.Text + "\n");
            }
        }

        // returns true if all works -- otherwise false
        private bool CombineAndLink(string file1, string file2)
        {
            if (isADIF(file1) && isADIF(file2))
            {
                // Let's read in file2 and strip out the header
                string[] lines = File.ReadAllLines(file2);
                // skip all lines until <call> occurs
                if (!lines[0].Contains("<call:", StringComparison.InvariantCultureIgnoreCase))
                {
                    lines = lines.Skip(1).ToArray();
                }
                File.AppendAllLines(file1, lines);
                File.Move(file2, file2 + ".bak");
                bool success = LinkFile.Program.linkFile(file1, file2);
                return true;
            }
            return false;
        }
        private void buttonFile2_Click(object sender, EventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                if (textBox2.Text.Length == 0)
                {
                    MessageBox.Show("No filename in File#2", "LinkFile", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    return;
                }
                if (File.Exists(textBox2.Text))
                {
                    MessageBox.Show("Cannot link to an existing file", "LinkFile", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    return;
                }
                bool success = LinkFile.Program.linkFile(textBox2.Text, textBox1.Text);
                if (success)
                {
                    MessageBox.Show("Link created successfully", "LinkFile", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    return;
                }
                else
                {
                    MessageBox.Show("Error creating link", "LinkFile", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    return;
                }
            }
            else if (ModifierKeys.HasFlag(Keys.Control))
            {
                if (!File.Exists(textBox2.Text))
                {
                    var reply1 = MessageBox.Show("Please select File#2 first", "LinkFile", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    if (reply1 == DialogResult.Cancel)
                        return;
                    if (!File.Exists(textBox1.Text))
                    {
                        MessageBox.Show("File#1 does not exists", "LinkFile", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                        return;
                    }
                    bool success = LinkFile.Program.linkFile(textBox1.Text, textBox2.Text);
                }
                if (textBox2.Text.Length == 0)
                {
                    MessageBox.Show("Please select File#2 first", "LinkFile", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    //MessageBox.Show("Please select File#2 first.","File#1");
                    return;
                }
                if (File.Exists(textBox2.Text + ".bak"))
                {
                    //MessageBox.Show("File already exists with a .bak suffix.  Please remove it first.");
                    MessageBox.Show("File already exists with a .bak suffix.  Please remove it first.", "LinkFile", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                    return;
                }
                var reply2 = MessageBox.Show("This will combine File#2 with File#1, rename file#2 with a .bak suffix, and make a hard link between the files.  Are you sure?", "Combine Files", MessageBoxButtons.YesNo);
                if (reply2 == DialogResult.No) return;
                if (CombineAndLink(textBox1.Text, textBox2.Text))
                {
                    //MessageBox.Show("Files combined and linked successfully.");
                    MessageBox.Show("Files combined and linked successfully.", "LinkFiles", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
                }
                else
                {
                    MessageBox.Show("Error combining files");
                }
                return;
            }
            else
            {
                textBox2.Text = getFileName(openFileDialog1.FileName, false);
                if (textBox2.Text.Length == 0) return;
            }
            if (textBox1.Text.Length == 0) return;
            if (File.Exists(textBox2.Text + ".bak"))
            {
                MessageBox.Show("File already exists with a .bak suffix.  Please remove it first.");
                return;
            }
            string md5sum2 = ComputeMD5Checksum(textBox2.Text);
            string md5sum1 = "";
            if (textBox1.Text.Length > 0) md5sum1 = ComputeMD5Checksum(textBox1.Text);
            if (md5sum1.Equals(md5sum2))
            {
                MessageBox.Show("Files are exactly the same!!  Already linked? Canceling combine...", "LinkFile", MessageBoxButtons.OK);
            }
            if (isADIF(textBox2.Text))
            {
                richTextBox1.AppendText("ADIF detected in " + textBox2.Text + "\n");
            }
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            if (e.LinkText is not null)
                LaunchWeblink(e.LinkText);
        }
        // Performs the actual browser launch to follow link:
        private void LaunchWeblink(string url)
        {
            if (IsHttpURL(url))
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });

        }

        // Simple check to make sure link is valid,
        // can be modified to check for other protocols:
        private bool IsHttpURL(string url)
        {
            Uri? uriResult;
            bool result = Uri.TryCreate(url, UriKind.Absolute, result: out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            if (!File.Exists(textBox1.Text))
            {
                MessageBox.Show("File does not exist", "LinkFile", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            }   
        }
    }
}
