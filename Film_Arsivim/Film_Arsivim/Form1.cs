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
using CefSharp;
using CefSharp.WinForms;

namespace Film_Arsivim
{
    public partial class Form1 : Form
    {
        private ChromiumWebBrowser chromeBrowser;
        public Form1()
        {
            InitializeComponent();
            Cef.Initialize(new CefSettings());
            #region Tarayıcı oluşturur
            chromeBrowser = new ChromiumWebBrowser("https://www.youtube.com");
            #endregion

            #region  Tarayıcıyı panelin içine ekler
            chromeBrowser.Dock = DockStyle.Fill;
            #endregion

            #region  Panel içine ekler
            panelBrowser.Controls.Add(chromeBrowser);
            #endregion
        }
        SqlConnection baglanti=new SqlConnection(@"Data Source=SINAN;Initial Catalog=FilmArsivim;Integrated Security=True;TrustServerCertificate=True");
        void Filmler()
        {
            SqlCommand komut = new SqlCommand("Select * From TBLFILMLER", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Filmler();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLFILMLER (AD,KATEGORI,LINK) values (@p1,@p2,@p3)",baglanti);
            komut.Parameters.AddWithValue("@p1",txtFilmAd.Text);
            komut.Parameters.AddWithValue("@p2",txtKategori.Text);
            komut.Parameters.AddWithValue("@p3",txtLink.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Film Listeye Kaydedildi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Filmler() ;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen=dataGridView1.SelectedCells[0].RowIndex;
            string link = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            chromeBrowser.Load(link);
        }

        private void btnTamEkran_Click(object sender, EventArgs e)
        {
            TamEkran tm = new TamEkran(chromeBrowser);
            tm.WindowState= FormWindowState.Maximized;
            tm.Show();
        }
    }
}
