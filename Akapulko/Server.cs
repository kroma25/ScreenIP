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
using System.Runtime.InteropServices; //myszka

namespace Akapulko
{
    
   
    public partial class Server : Form
    {
        private readonly TcpClient client = new TcpClient();
        private NetworkStream mainStream;
        private int portNumber;


        private static Image GrabDesktop()
        {
            //https://developingsoftware.com/capture-screenshot-include-cursor/  - fajnie opisanie zachowanie tego modulu
            //lapanie obrazu przez screenshoty
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppPArgb);
            Graphics graphic = Graphics.FromImage(screenshot);
            graphic.CopyFromScreen(0, 0, 0, 0, screenshot.Size, CopyPixelOperation.SourceCopy);

            //rozdzielcznosc rzeczywista jest sprawdzana na komputerze i ustawina potem jest konwertowan na rodzielczosc virtualna!!

            User32.CURSORINFO cursorInfo;
            cursorInfo.cbSize = Marshal.SizeOf(typeof(User32.CURSORINFO));
            if (User32.GetCursorInfo(out cursorInfo))
            {
                // if the cursor is showing draw it on the screen shot
                if (cursorInfo.flags == User32.CURSOR_SHOWING)
                {
                    // we need to get hotspot so we can draw the cursor in the correct position
                    var iconPointer = User32.CopyIcon(cursorInfo.hCursor);
                    User32.ICONINFO iconInfo;
                    int iconX, iconY;

                    if (User32.GetIconInfo(iconPointer, out iconInfo))
                    {
                        // calculate the correct position of the cursor
                        iconX = cursorInfo.ptScreenPos.x - ((int)iconInfo.xHotspot);
                        iconY = cursorInfo.ptScreenPos.y - ((int)iconInfo.yHotspot);

                        // draw the cursor icon on top of the captured screen image
                        User32.DrawIcon(graphic.GetHdc(), iconX, iconY, cursorInfo.hCursor);

                        // release the handle created by call to g.GetHdc()
                        graphic.ReleaseHdc();
                    }
                }
            }
            return screenshot;
        }
        private void SendDesktopImage()
        {
            //wysylanie obrazu do pary:P
            
                BinaryFormatter binFormatter = new BinaryFormatter();
                mainStream = client.GetStream();
                binFormatter.Serialize(mainStream, GrabDesktop());
           
        }
        public Server()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //Sprawdzenie czy uda się połaczyć z serwerem
            try
            { 
            portNumber = int.Parse(txtPort.Text);
            }
            catch(Exception)
            {
                MessageBox.Show("Błędne dane!");
            }
            try
            {
                client.Connect(txtIp.Text, portNumber);
                MessageBox.Show("Połączono!");
                btnSend.Enabled = true; 
                btnAutoSearch.Enabled = false;
                btnConnect.Enabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Internet z biedronki...");
                //throw; wyrzucanie aplikacji
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //Wysyłanie obrazu  
            if (btnSend.Text.StartsWith("Udostępnij ekran"))
            {
                timer1.Start();
                btnSend.Text = "Przerwij transmisje";
            }
            else
            {
                timer1.Stop();
                btnSend.Text = "Udostępnij ekran";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //w zasadzie to sluzy tylko do wywolania funkcji
            //Wartość domyślna to 100 milisekund. zwiekszenie tej opcji spowoduje zwiekszenie ilosci klatek
            timer1.Interval = 50;
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
            txtIp.Text = ServerEp.Address.ToString();
            txtPort.Text = ServerResponse;
            Client.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //przesuwanie okna wszedzie
        //https://stackoverflow.com/questions/1592876/make-a-borderless-form-movable
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;
    }
    //1. Potrzebne do obslugi modul myszki
    public static class User32
    {
        public const Int32 CURSOR_SHOWING = 0x00000001;

        [StructLayout(LayoutKind.Sequential)]
        public struct ICONINFO
        {
            public bool fIcon;
            public Int32 xHotspot;
            public Int32 yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public Int32 x;
            public Int32 y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CURSORINFO
        {
            public Int32 cbSize;
            public Int32 flags;
            public IntPtr hCursor;
            public POINT ptScreenPos;
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorInfo(out CURSORINFO pci);

        [DllImport("user32.dll")]
        public static extern IntPtr CopyIcon(IntPtr hIcon);

        [DllImport("user32.dll")]
        public static extern bool DrawIcon(IntPtr hdc, int x, int y, IntPtr hIcon);

        [DllImport("user32.dll")]
        public static extern bool GetIconInfo(IntPtr hIcon, out ICONINFO piconinfo);
    }
}

