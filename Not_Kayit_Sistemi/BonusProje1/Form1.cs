﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BonusProje1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pbOgrenci_Click(object sender, EventArgs e)
        {
            FrmOgrenciNotlar frm = new FrmOgrenciNotlar();
            frm.numara=txtOgrenciNo.Text;
            frm.Show();
        }

        private void pbOgretmen_Click(object sender, EventArgs e)
        {
            FrmOgretmen frm = new FrmOgretmen();
            frm.Show();
            this.Hide();
        }
    }
}
