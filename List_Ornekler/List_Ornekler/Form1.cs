using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace List_Ornekler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnListele1_Click(object sender, EventArgs e)
        {
            List<string> kisiler = new List<string>();
            kisiler.Add("Ali");
            kisiler.Add("Mehmet");
            kisiler.Add("Zeynep");
            kisiler.Add("Murat");
            kisiler.Add("Sinan");
            kisiler.Add("Kadir");
            kisiler.Remove("Ali");
            foreach (string s in kisiler)
            {
                listBox1.Items.Add(s);
            }
        }

        private void btnListele2_Click(object sender, EventArgs e)
        {
            List<int> sayilar = new List<int>();
            sayilar.Add(12);
            sayilar.Add(36);
            sayilar.Add(25);
            sayilar.Add(85);
            sayilar.Add(20);
            sayilar.Add(9);
            foreach (int h in sayilar)
            {
                if (h % 5 == 0)
                {
                    listBox2.Items.Add(h);
                }
            }
            if (sayilar.Contains(Convert.ToInt32(textBox1.Text)))
            {
                MessageBox.Show("Bu sayı listede var");
            }
            else
            {
                MessageBox.Show("Bu sayı listede yok");
            }
        }

        private void btnListele3_Click(object sender, EventArgs e)
        {
            List<Kisiler> kisi = new List<Kisiler>();
            kisi.Add(new Kisiler()
            {
                ADI = "Veysel",
                SOYADI = "Kınay",
                MEKLEKI = "Mühendis"

            });
            foreach (Kisiler k in kisi)
            {
                listBox3.Items.Add(k.ADI + " " + k.SOYADI + " " + k.MEKLEKI);
            }
        }

        private void btnListele4_Click(object sender, EventArgs e)
        {
            List<Arabalar> araba = new List<Arabalar>();
            araba.Add(new Arabalar()
            {
                Markası = "BMW",
                Modeli = "3.20",
                Fiyati = 3000000
            });
            foreach (Arabalar a in araba)
            {
                listBox4.Items.Add(a.Markası + " " + a.Modeli + " " + a.Fiyati);
            }
        }
    }
}
