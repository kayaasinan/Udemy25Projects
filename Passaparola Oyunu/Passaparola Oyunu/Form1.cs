using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Passaparola_Oyunu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int soruno = 0, dogru = 0, yanlis = 0;
        string[] sorular = new string[]
        {
            "Elması ile meşhur ilimiz neresidir?",
            "Erdek ilçesi hangi ilin ilçesidir?",
            "Adana'nın en kalabalık ilçesi neresidir?",
            "Yesterday kelimesinin anlamı?",
            "Yeni kelimesinin zıt anlamlısı nedir?",
            "Horoz simgeli ülke neresidir?",
            "Fındığı ile meşhur ilimiz neresidir?",
            "Künefesi ile meşhur ilimiz neresidir?",
            "Hedefi vuramamak anlamındaki söz nedir?",
            "Türkiye'nin en kalalık şehri neresidir?",
            "Askeri kara birliği?",
            "Malatyanın meşhur meyvesi nedir?",
            "Osmanlı'da zevk ve sefa devri?",
            "Yılın üçüncü ayı nedir?",
            "Üflemeli bir müzik aleti?",
            "Halk şairini eş anlamlısı nedir?",
            "Bir çeşit sebze?",
            "11 ayın sultanı",
            "İngilizcede benzer kelimesinin anlamı?",
            "Türkiye'nin megastarı kimdir?",
            "Bir çeşit balık?",
            "Bir fizik dersi konusu?",
            "Doğru kelimesinin zıt anlamlısı?",
            "Kömür madenleriyle ünlü ilimiz neresidir?"
        };
        string[] cevaplar = new string[]
        {
            "amasya",
            "balıkesir",
            "ceyhan",
            "dün",
            "eski",
            "fransa",
            "giresun",
            "hatay",
            "ıskalamak",
            "istanbul",
            "jandarma",
            "kayısı",
            "lale",
            "mart",
            "ney",
            "ozan",
            "pırasa",
            "ramazan",
            "similar",
            "tarkan",
            "uskumru",
            "vektörler",
            "yanlış",
            "zonguldak"
        };
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkLabel1.Text == "Başla")
            {
                linkLabel1.Text = "Sonraki";
                soruno = 0;
                richTextBox1.Text = sorular[soruno];
                return;
            }
            if (linkLabel1.Text == "Sonraki")
            {
                string butonAdi = "button" + (soruno + 1).ToString();
                string verilenCevap = textBox1.Text.ToLower();

                if (verilenCevap == cevaplar[soruno])
                {
                    dogru++;
                    this.Controls[butonAdi].BackColor = Color.Green;
                }
                else
                {
                    yanlis++;
                    this.Controls[butonAdi].BackColor = Color.Red;
                }
                soruno++;
                textBox1.Clear();
                if (soruno < sorular.Length)
                {
                    richTextBox1.Text = sorular[soruno];
                    string yeniButon = "button" + (soruno + 1).ToString();
                    this.Controls[yeniButon].BackColor = Color.Yellow;
                }
                else
                {
                    MessageBox.Show("Oyun bitti!\nDoğru: " + dogru + "\nYanlış: " + yanlis);
                    linkLabel1.Enabled = false;
                }
                lblDogru.Text = dogru.ToString();
                lblYanlis.Text = yanlis.ToString();
            }
        }
    }
}
