using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Akapulko
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            

        }
    }
    
}
/*
 * Parametry urchominiea programu są poprawione tzn. program uruchamia sie jako zminimalizowany
 *  FormBorderStyle:none, ShowIcon:false, ShowInTaskbar:false, Opacity:0%, WindowState:Minimized
 *  Dzięki temu mamy Tray można by to zrobić inaczej ale na szybko to wystarczy
 *  
 * 
 */
