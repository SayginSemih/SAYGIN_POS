using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SAYGIN_POS
{
    public partial class Detaylar : Form
    {
        public Detaylar()
        {
            InitializeComponent();
        }

        public string connectionString = "server=localhost;user=root;database=cafeteria;password=";
        public void GetLog()
        {
            // SQL sorgusu
            string query = "SELECT id as 'ID',urun as 'SATILAN ÜRÜNLER',fiyat as 'FİYATLAR' FROM log";

            // SQL bağlantısı ve komut nesneleri
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);

            // Veri deposu (DataTable) oluşturun
            DataTable dataTable = new DataTable();

            // Verileri SQL sorgusuyla veritabanından alın
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            adapter.Fill(dataTable);

            // DataGridView'e veri deposunu bağlayın
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }
        private void Detaylar_Load(object sender, EventArgs e)
        {
            GetLog();
        }

        private void btnSec_Click(object sender, EventArgs e)
        {
            // SQL sorgusu
            string query = "SELECT SUM(fiyat) AS toplam_satis_fiyati FROM log WHERE urun=@p1";

            // SQL bağlantısı ve komut nesneleri
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@p1", txtUrun.Text);
                MySqlDataReader rd = command.ExecuteReader();
                if (rd.Read())
                {   
                    lblKar.Text = txtUrun.Text + " adlı üründen elde edilen toplam kar " + rd[0].ToString() + " TL";
                }
                rd.Close();
                connection.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("HATA : " + exc.Message);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            // SQL sorgusu
            string query = "DELETE FROM log";

            // SQL bağlantısı ve komut nesneleri
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Tüm loglar başarıyla silindi!","SAYGIN POS");
                connection.Close();
                GetLog();
            }
            catch (Exception exc)
            {
                MessageBox.Show("HATA : " + exc.Message);
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Show();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font font=new Font("Arial", 14, FontStyle.Bold);
            SolidBrush firca = new SolidBrush(Color.Black);
            e.Graphics.DrawString($"Tarih : {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}",font,firca,2,10);
            font = new Font("Arial", 20 ,FontStyle.Bold);
            e.Graphics.DrawString("DETAYLAR", font, firca, 320, 50);
            e.Graphics.DrawString("***********************", font, firca, 270, 90);
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT urun as ÜRÜN,SUM(fiyat) as 'TOPLAM KAR' FROM log GROUP BY urun", con);
            MySqlDataReader rdd = cmd.ExecuteReader();
            int i = 120;
            while (rdd.Read())
            {
                font = new Font("Arial", 14, FontStyle.Bold);
                e.Graphics.DrawString("Ürün : ", font, firca, 200, i);
                font = new Font("Arial", 14);
                e.Graphics.DrawString(rdd[0].ToString(), font, firca, 300, i);
                font = new Font("Arial", 14, FontStyle.Bold);
                e.Graphics.DrawString("Gelir : ", font, firca, 200, i+40);
                font = new Font("Arial", 14);
                e.Graphics.DrawString(rdd[1].ToString() + " TL", font, firca, 300, i+40);
                font = new Font("Arial", 14);
                e.Graphics.DrawString("-------------------------------------------", font, firca, 200, i + 60);
                i = i + 75;
            }
            rdd.Close();
            con.Close();
            string query = "SELECT SUM(fiyat) FROM log";

            // SQL bağlantısı ve komut nesneleri
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                font = new Font("Arial", 14, FontStyle.Bold);
                e.Graphics.DrawString("Toplam Gelir : ", font, firca, 200, i+40);
                font = new Font("Arial", 14);
                e.Graphics.DrawString(reader[0].ToString() + " TL", font, firca, 350, i+40);
            }
            reader.Close();
            connection.Close();
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
