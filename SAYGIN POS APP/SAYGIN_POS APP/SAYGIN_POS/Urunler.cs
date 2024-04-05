using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAYGIN_POS
{
    public partial class Urunler : Form
    {
        public Urunler()
        {
            InitializeComponent();
        }
        private void btnAlternatifSogukIcecekler_Click(object sender, EventArgs e)
        {
            UrunMenu um = new UrunMenu();
            um.catagory = 5001;
            um.Show();
        }

        private void btnAtistirmalar_Click(object sender, EventArgs e)
        {
            UrunMenu um = new UrunMenu();
            um.catagory = 5002;
            um.Show();
        }

        private void btnCerez_Click(object sender, EventArgs e)
        {
            UrunMenu um = new UrunMenu();
            um.catagory = 5003;
            um.Show();
        }

        private void btnDondurmalar_Click(object sender, EventArgs e)
        {
            UrunMenu um = new UrunMenu();
            um.catagory = 5004;
            um.Show();
        }

        private void btnFrozen_Click(object sender, EventArgs e)
        {
            UrunMenu um = new UrunMenu();
            um.catagory = 5005;
            um.Show();
        }

        private void btnKahveCesitleri_Click(object sender, EventArgs e)
        {
            UrunMenu um = new UrunMenu();
            um.catagory = 5006;
            um.Show();
        }

        private void btnMakarnalar_Click(object sender, EventArgs e)
        {
            UrunMenu um = new UrunMenu();
            um.catagory = 5007;
            um.Show();
        }

        private void btnMeyveTabagi_Click(object sender, EventArgs e)
        {
            UrunMenu um = new UrunMenu();
            um.catagory = 5008;
            um.Show();
        }

        private void btnMilkshake_Click(object sender, EventArgs e)
        {
            UrunMenu um = new UrunMenu();
            um.catagory = 5009;
            um.Show();
        }

        private void btnNargileler_Click(object sender, EventArgs e)
        {
            UrunMenu um = new UrunMenu();
            um.catagory = 5010;
            um.Show();
        }

        private void btnPizzalar_Click(object sender, EventArgs e)
        {
            UrunMenu um = new UrunMenu();
            um.catagory = 5011;
            um.Show();
        }

        private void btnSalatalar_Click(object sender, EventArgs e)
        {
            UrunMenu um = new UrunMenu();
            um.catagory = 5012;
            um.Show();
        }

        private void btnSicakIcecekler_Click(object sender, EventArgs e)
        {
            UrunMenu um = new UrunMenu();
            um.catagory = 5013;
            um.Show();
        }

        private void btnSogukIcecekler_Click(object sender, EventArgs e)
        {
            UrunMenu um = new UrunMenu();
            um.catagory = 5014;
            um.Show();
        }

        private void btnSogukKahveler_Click(object sender, EventArgs e)
        {
            UrunMenu um = new UrunMenu();
            um.catagory = 5015;
            um.Show();
        }

        private void btnTapTazeIcecekler_Click(object sender, EventArgs e)
        {
            UrunMenu um = new UrunMenu();
            um.catagory = 5016;
            um.Show();
        }

        private void btnTatlilar_Click(object sender, EventArgs e)
        {
            UrunMenu um = new UrunMenu();
            um.catagory = 5017;
            um.Show();
        }

        private void btnTavukMakarnaMenu_Click(object sender, EventArgs e)
        {
            UrunMenu um = new UrunMenu();
            um.catagory = 5018;
            um.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox2.Width++;
            pictureBox2.Height++;
            if (pictureBox2.Height % 2 == 0) { pictureBox2.Location = new Point(pictureBox2.Location.X - 1, pictureBox2.Location.Y - 1); }
            if (pictureBox2.Height >= 26) { timer1.Stop(); pictureBox2.Size = new Size(26, 26); }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            pictureBox2.Width--;
            pictureBox2.Height--;
            if (pictureBox2.Height % 2 == 0) { pictureBox2.Location = new Point(pictureBox2.Location.X + 1, pictureBox2.Location.Y + 1); }
            if (pictureBox2.Height <= 20) { timer2.Stop(); pictureBox2.Size = new Size(20, 20); }
        }


        private void timer3_Tick(object sender, EventArgs e)
        {
            pictureBox3.Width++;
            pictureBox3.Height++;
            if (pictureBox3.Height % 2 == 0) { pictureBox3.Location = new Point(pictureBox3.Location.X - 1, pictureBox3.Location.Y - 1); }
            if (pictureBox3.Height >= 26) { timer3.Stop(); pictureBox3.Size = new Size(26, 26); }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            pictureBox3.Width--;
            pictureBox3.Height--;
            if (pictureBox3.Height % 2 == 0) { pictureBox3.Location = new Point(pictureBox3.Location.X + 1, pictureBox3.Location.Y + 1); }
            if (pictureBox3.Height <= 20) { timer4.Stop(); pictureBox3.Size = new Size(20, 20); }
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            timer4.Stop();
            timer3.Start();
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            timer3.Stop();
            timer4.Start();
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            timer2.Stop();
            timer1.Start();
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            timer1.Stop();
            timer2.Start();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
