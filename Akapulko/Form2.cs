using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary; //odpowiadajacy za konwersje obrazu
using System.Net.Sockets; //za siec
using System.Drawing.Imaging; // za obraz
using System.Drawing;
using System.Net;

namespace Akapulko
{
    public partial class Form2 : Form 
    {
        private readonly TcpClient client = new TcpClient();
        private NetworkStream mainStream;
        private int portNumber;


        private static Image GrabDesktop()
        {
            //lapanie obrazu przez screenshoty
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppPArgb);
            Graphics graphic = Graphics.FromImage(screenshot);
            graphic.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
            return screenshot;

        }
        private void SendDesktopImage()
        {
            //wysylanie obrazu do pary:P
            BinaryFormatter binFormatter = new BinaryFormatter();
            mainStream = client.GetStream();
            binFormatter.Serialize(mainStream, GrabDesktop());

        }
        public Form2()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //Sprawdzenie czy uda się połaczyć z serwerem
            portNumber = int.Parse(txtPort.Text);
            try
            {
                client.Connect(txtIp.Text, portNumber);
                MessageBox.Show("Połączono!");
            }
            catch (Exception)
            {
                MessageBox.Show("Internet z biedronki...");
                throw;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //Wysyłanie obrazu
            if (btnSend.Text.StartsWith("Udostępnij Ekran"))
            {
                timer1.Start();
                btnSend.Text = "Przerwij transmisje";
            }
            else
            {
                timer1.Stop();
                btnSend.Text = "Udostępnij Ekran";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //w zasadzie to sluzy tylko do wywolania funkcji
            SendDesktopImage();
        }

        private void btnAutoSearch_Click(object sender, EventArgs e)
        {
            //sprawdzenie czy ktos nie nadaje
            // https://stackoverflow.com/questions/22852781/how-to-do-network-discovery-using-udp-broadcast
            //odchudzic kod nie potrzeben jest odslanie po udp
            var Client = new UdpClient();
            var RequestData = Encoding.ASCII.GetBytes("SomeRequestData");
            var ServerEp = new IPEndPoint(IPAddress.Any, 0);

            Client.EnableBroadcast = true;
            Client.Send(RequestData, RequestData.Length, new IPEndPoint(IPAddress.Broadcast, 8888));

            var ServerResponseData = Client.Receive(ref ServerEp);
            var ServerResponse = Encoding.ASCII.GetString(ServerResponseData);
            //Console.WriteLine("Recived {0} from {1}", ServerResponse, ServerEp.Address.ToString());

            //przypodkowanie adresu i portu
            txtIp.Text= ServerEp.Address.ToString();
            txtPort.Text= ServerResponse;           
            Client.Close();
        }
    }
}
