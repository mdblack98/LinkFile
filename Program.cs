using System.Runtime.InteropServices;

namespace LinkFile
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]

        static extern bool CreateHardLink(string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes);

        [STAThread]

        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }

        static public bool linkFile(string file1, string file2)
        {
            return CreateHardLink(file1, file2, IntPtr.Zero);
        }
    }
}