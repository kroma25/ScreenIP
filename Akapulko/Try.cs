using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Akapulko
{
    public partial class Try : Form
    {
        public Try()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close(); //Zamykanie aplikacji
            System.Windows.Forms.Application.Exit(); //ubijanie aplikacji hard
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void contextMenuStrip3_Opening(object sender, CancelEventArgs e)
        {

        }

        private void ustawieniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Klient wnd = new Klient();
            wnd.Show();
        }

        private void serwerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Serwer wnd = new Serwer();
            wnd.Show();
        }
    }
}
