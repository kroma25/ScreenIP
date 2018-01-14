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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close(); //Zamykanie aplikacji
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
        }

        private void contextMenuStrip3_Opening(object sender, CancelEventArgs e)
        {

        }

        private void ustawieniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 wnd = new Form2();
            wnd.Show();
        }

        private void serwerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 wnd = new Form3();
            wnd.Show();
        }
    }
}
