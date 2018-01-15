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
        Thread UDPBroadcasting;
        public Form4(int Port)
        {
            port = Port;
            client = new TcpClient();
            UDPBroadcasting = new Thread(UDPBroadcast);
            Listening = new Thread(StartListening);
            GetImage = new Thread(ReceiveImage);
            InitializeComponent();
            UDPBroadcasting.Start();//uruchomienie wątku 2h szukania ...
            //pictureBox1.Size = new System.Drawing.Size(1536, 960);
            
        }
        private void UDPBroadcast()
        {
            //brodcast servera po sieci
            // https://stackoverflow.com/questions/22852781/how-to-do-network-discovery-using-udp-broadcast
            
            var ServerUDP = new UdpClient(8888);
            var ResponseData = Encoding.ASCII.GetBytes("" + port);
            //tutaj trzeba dopracowac serwer ma nadawc caly czas albo jak serwer odpowie
            //odchudzic kod nie potrzebuje wszystkiego z tąd
            while (true)
            {
                var ClientEp = new IPEndPoint(IPAddress.Any, 0);
                var ClientRequestData = ServerUDP.Receive(ref ClientEp);
                var ClientRequest = Encoding.ASCII.GetString(ClientRequestData);

                //Console.WriteLine("Recived {0} from {1}, sending response", ClientRequest, ClientEp.Address.ToString());
                ServerUDP.Send(ResponseData, ResponseData.Length, ClientEp);
            }
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
                //testy
                Image temp = (Image)binFormatter.Deserialize(mainStream);
                int imageHeight = temp.Height; //960
                int imageWidth = temp.Width; //1536
                //koniec testow
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
