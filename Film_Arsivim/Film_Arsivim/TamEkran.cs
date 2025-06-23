using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Film_Arsivim
{
    public partial class TamEkran : Form
    {
        public TamEkran(ChromiumWebBrowser browser)
        {
            InitializeComponent();
            browser.Dock = DockStyle.Fill;
            this.panelTamEkran.Controls.Add(browser);
        }

        private void TamEkran_Load(object sender, EventArgs e)
        {

        }
    }
}
