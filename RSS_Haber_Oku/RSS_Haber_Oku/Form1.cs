using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace RSS_Haber_Oku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        void RssHaber(string haberUrl)
        {
            listBox1.Items.Clear();
            XmlTextReader xmlOku = new XmlTextReader(haberUrl);
            while (xmlOku.Read())
            {
                if (xmlOku.Name == "title")
                {
                    listBox1.Items.Add(xmlOku.ReadString());
                }
            }
        }
        private void btnHurriyet_Click(object sender, EventArgs e)
        {
            RssHaber("https://www.hurriyet.com.tr/rss/anasayfa");
        }
        private void btnSozcu_Click(object sender, EventArgs e)
        {
            RssHaber("https://www.sozcu.com.tr/feeds-rss-category-sozcu");
        }
        private void btnBbc_Click(object sender, EventArgs e)
        {
            RssHaber("https://feeds.bbci.co.uk/turkce/rss.xml");
        }
    }
}
