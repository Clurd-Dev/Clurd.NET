using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;

namespace Clurd.APP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            if(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/server.config"))
            {
                var pathraw = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/server.config");
                pathclurd = pathraw;
                this.path.Text = pathraw;   
                
            }
        }

        bool local = true;
        bool all = true;
        string pathclurd = "";
        private void start_Click_1(object sender, EventArgs e)
        {
            string localIP = "";
            if (local == true)
            {
                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                {
                    socket.Connect("8.8.8.8", 65530);
                    IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                    localIP = endPoint.Address.ToString();
                }
            }
            if(all == true)
            {
                localIP = "0.0.0.0";
            }
            System.Diagnostics.Process.Start("CMD.exe", "/K cd " + pathclurd + "& start Clurd.exe --urls http://" + localIP + ":80");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            local = !local;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            all = !all;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("Clurd"))
            {
                process.Kill();
            }
        }

        private void select_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    pathclurd = fbd.SelectedPath;
                    File.WriteAllTextAsync(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/server.config", pathclurd);
                    this.path.Text = fbd.SelectedPath;
                }
            }
        }

        private void editconfig_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", pathclurd + "/app.config");
        }
    }
}