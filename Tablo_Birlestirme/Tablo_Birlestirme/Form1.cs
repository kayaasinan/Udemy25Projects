using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Reflection;

namespace Tablo_Birlestirme
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'iliskiliTablolarDataSet.Proje6' table. You can move, or remove it, as needed.
            this.proje6TableAdapter.Fill(this.iliskiliTablolarDataSet.Proje6);

        }
    }
}
