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


namespace Akapulko
{
    public partial class Serwer : Form
    {
        public Serwer()
        {
            
            InitializeComponent();
            label2.Text = "IP(WiFi): " + GetLocalIPv4(NetworkInterfaceType.Wireless80211);
            label3.Text = "IP(Eth): " + GetLocalIPv4(NetworkInterfaceType.Ethernet);
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            int port = int.Parse(txtPort.Text);
            Podglad f4 = new Podglad(port);
            f4.Show();  
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
       
    }
}
