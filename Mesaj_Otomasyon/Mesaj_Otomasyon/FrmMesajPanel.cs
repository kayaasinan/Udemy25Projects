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

namespace Mesaj_Otomasyon
{
    public partial class FrmMesajPanel : Form
    {
        public FrmMesajPanel()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=SINAN;Initial Catalog=Mesajlasma;Integrated Security=True;TrustServerCertificate=True");
        public string numara;
        void GelenKutusu()
        {
            SqlCommand komut = new SqlCommand("GelenTest", baglanti);
            komut.CommandType = CommandType.StoredProcedure;
            komut.Parameters.AddWithValue("@ALICI", numara);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void GidenKutusu()
        {
            SqlCommand komut = new SqlCommand("GidenTest", baglanti);
            komut.CommandType = CommandType.StoredProcedure;
            komut.Parameters.AddWithValue("@GONDEREN", numara);
            SqlDataAdapter da1 = new SqlDataAdapter(komut);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            dataGridView2.DataSource = dt1;
        }
        private void FrmMesajPanel_Load(object sender, EventArgs e)
        {
            lblNumara.Text = numara;
            GelenKutusu();
            GidenKutusu();

            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLKISILER Where NUMARA=" + numara,baglanti);
            SqlDataReader dr=komut.ExecuteReader();
            while(dr.Read())
            {
                lblAdSoyad.Text = dr[1]+" "+dr[2];
            }
            baglanti.Close();
        }

        private void FrmMesajPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLMESAJLAR (GONDEREN,ALICI,BASLIK,ICERIK) values (@p1,@p2,@p3,@p4)",baglanti);
            komut.Parameters.AddWithValue("@p1",numara);
            komut.Parameters.AddWithValue("@p2",mtbAlici.Text);
            komut.Parameters.AddWithValue("@p3", txtBaslik.Text);
            komut.Parameters.AddWithValue("@p4",richTextBox1.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("İletiniz Gönderildi");
            GelenKutusu();
            GidenKutusu();
        }
    }
}
