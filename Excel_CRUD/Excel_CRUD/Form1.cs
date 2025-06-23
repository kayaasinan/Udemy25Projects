using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Excel_CRUD
{
    public partial class Form1 : Form
    {
        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\sinankaya\Excel\Formül Açıklama.xlsx;Extended Properties='Excel 12.0 Xml;HDR=YES;'");

        public Form1()
        {
            InitializeComponent();
        }
        void Listele()
        {
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("Select * From [Sayfa1$]", baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {

              MessageBox.Show("HATA:" + ex.Message);
            }

        }
        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("insert into [sayfa1$] (FONKSİYONENG,FONKSİYONTR) values (@p1,@p2)",baglanti);
            komut.Parameters.AddWithValue("@p1",textBox1.Text);
            komut.Parameters.AddWithValue("@p2",textBox2.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Yeni Fonksiyon Eklendi");
            Listele();
        }
    }
}
