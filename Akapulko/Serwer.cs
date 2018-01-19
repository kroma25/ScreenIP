using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Threading;



namespace Akapulko
{
    
    public partial class Serwer : Form
    {
        public Thread UDPBroadcasting;
        
        
        public Serwer()
        {
            
            InitializeComponent();
            label2.Text = "IP(WiFi): " + GetLocalIPv4(NetworkInterfaceType.Wireless80211);
            label3.Text = "IP(Eth): " + GetLocalIPv4(NetworkInterfaceType.Ethernet);
            UDPBroadcasting = new Thread(UDPBroadcast);
            
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            
            int portNumber;
            if (int.TryParse(txtPort.Text, out portNumber)
                && portNumber >= 1
                && portNumber <= 65535)
            {
                // sprawdzenie czy jest odpowiedni port
                int port = int.Parse(txtPort.Text);
                Podglad f4 = new Podglad(port);
                f4.Show();
                if (UDPBroadcasting.IsAlive == false)
                {
                    UDPBroadcasting.Start();
                }
                
            }
            else
            {
                MessageBox.Show("Błąd wartości, proszę podać numer portu z przedziału 1 - 65535");
            }
            
            //UDP caly czas nadaje
           
            

        }
       
        private void UDPBroadcast()
        {
            //brodcast servera po sieci
            // https://stackoverflow.com/questions/22852781/how-to-do-network-discovery-using-udp-broadcast
            var ServerUDP = new UdpClient(8888);
            var ResponseData = Encoding.ASCII.GetBytes("" + int.Parse(txtPort.Text));
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

        public string GetLocalIPv4(NetworkInterfaceType _type)
        //https://stackoverflow.com/questions/6803073/get-local-ip-address


        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }

        //przesuwanie okna wszedzie
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
                  
        }
    }
}
