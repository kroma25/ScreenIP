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
    public partial class Podglad : Form
    {
        private readonly int portTCP;
        private TcpClient clientTCP;
        private TcpListener serverTCP;
        private NetworkStream mainStreamTCP;

        private readonly Thread Listening;
        private readonly Thread GetImage;

        // public Thread UDPBroadcasting { get; set; }

        //private Serwer Serwer; #UTP


        public Podglad(int Port)
        {
            portTCP = Port;
            clientTCP = new TcpClient();
            //UDPBroadcasting = new Thread(UDPBroadcast);
            Listening = new Thread(StartListening);
            GetImage = new Thread(ReceiveImage);
            InitializeComponent();
            
            //pictureBox1.Size = new System.Drawing.Size(1536, 960);
            //UDPBroadcasting.Start();//uruchomienie wątku 2h szukania ... 
                    
        }
        private void UDPBroadcast()
        {
            //brodcast servera po sieci
            // https://stackoverflow.com/questions/22852781/how-to-do-network-discovery-using-udp-broadcast
            var ServerUDP = new UdpClient(8888);
            var ResponseData = Encoding.ASCII.GetBytes("" + portTCP);
            //tutaj trzeba dopracowac serwer ma nadawc caly czas albo jak serwer odpowie
            //odchudzic kod nie potrzebuje wszystkiego z tąd
            while (true)
            {
                var ClientEp = new IPEndPoint(IPAddress.Any, 0);
                var ClientRequestData = ServerUDP.Receive(ref ClientEp);
                var ClientRequest = Encoding.ASCII.GetString(ClientRequestData);
                ServerUDP.Send(ResponseData, ResponseData.Length, ClientEp);
            }
        }
        private void StartListening()
        {
            while (clientTCP.Connected == false)
            {
                serverTCP.Start();
                clientTCP = serverTCP.AcceptTcpClient();
                
            }
            GetImage.Start();
            

        }
        private void StopListening()
        {
            serverTCP.Stop();
            clientTCP = null;
            
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
            while (clientTCP.Connected)
            {
                mainStreamTCP = clientTCP.GetStream();
                pictureBox1.Image = (Image)binFormatter.Deserialize(mainStreamTCP);
                //testy
                Image temp = (Image)binFormatter.Deserialize(mainStreamTCP);
                int imageHeight = temp.Height; //960
                int imageWidth = temp.Width; //1536
                //koniec testow
                //BroadcastTurnOFF(null);#UTP
                  
               
            }
        }
        /* #UTP
         void BroadcastTurnOFF(Serwer serwer)
        {
            if (serwer.UDPBroadcasting.IsAlive == true)
            {
                serwer.UDPBroadcasting.Abort();
            }
                        
        }
        */
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            serverTCP = new TcpListener(IPAddress.Any, portTCP);
            Listening.Start();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            StopListening();
            
        }
    }
}
