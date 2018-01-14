using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

namespace Akapulko
{
    public partial class Form4 : Form
    {
        private readonly int port;
        private TcpClient client;
        private TcpListener server;
        private NetworkStream mainStream;

        private readonly Thread Listening;
        private readonly Thread GetImage;

        public Form4(int Port)
        {
            port = Port;
            client = new TcpClient();
            Listening = new Thread(StartListening);
            GetImage = new Thread(ReceiveImage);
            InitializeComponent();
        }

        private void StartListening()
        {
            while (client.Connected == false)
            {
                server.Start();
                client = server.AcceptTcpClient();
            }
            GetImage.Start();
        }
        private void StopListening()
        {
            server.Stop();
            client = null;
            if (Listening.IsAlive == true)
            {
                Listening.Abort();
            }
            if (GetImage.IsAlive == true)
            {
                GetImage.Abort();
            }
        }
        private void ReceiveImage()
        {
            BinaryFormatter binFormatter = new BinaryFormatter();
            while (client.Connected)
            {
                mainStream = client.GetStream();
                pictureBox1.Image = (Image)binFormatter.Deserialize(mainStream);
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            server = new TcpListener(IPAddress.Any, port);
            Listening.Start();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            StopListening();
        }
    }
}
