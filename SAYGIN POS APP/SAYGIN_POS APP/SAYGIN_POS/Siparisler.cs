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
    public partial class Siparisler : Form
    {
        public Siparisler()
        {
            InitializeComponent();
        }

        public int zilkontrol = -1;
        public int i = 0;

        public string connectionString = "server=localhost;user=root;database=cafeteria;password=";
        public void GetOrders()
        {
            // SQL sorgusu
            string query = "SELECT id as 'ID',urun as 'ÜRÜN',fiyat as 'FİYAT', masa as 'MASA NUMARASI', ip as 'SİPARİŞ EDEN KİŞİNİN İP ADRESİ', musterinotu as 'MÜŞTERİ NOTU', durum as 'DURUM' FROM orders";

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
        private void Siparisler_Load(object sender, EventArgs e)
        {
            timer1.Start();
            dataGridView1.AllowUserToAddRows = false;
            this.Text = "SAYGIN POS - Siparişler";
            GetOrders();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(connectionString);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT Count(*) FROM orders", con);
            MySqlDataReader rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                i = Convert.ToInt16(rd[0]);
                if (i != zilkontrol)
                {
                    zilkontrol = i;
                    mediaplayer.URL = "zil.mp3";
                }
            }
            rd.Close();
            con.Close();
            GetOrders();
        }

        private void btnSec_Click(object sender, EventArgs e)
        {
            if (txtMasa.Text=="")
            {
                timer1.Start();
                GetOrders();
                lblMasa.Text = "-";
                lblUcret.Text = "0 TL";
            }
            else
            {
                timer1.Stop();
                // SQL sorgusu
                string query = "SELECT id as 'ID',urun as 'ÜRÜN',fiyat as 'FİYAT', masa as 'MASA NUMARASI', ip as 'SİPARİŞ EDEN KİŞİNİN İP ADRESİ', musterinotu as 'MÜŞTERİ NOTU', durum as 'DURUM' FROM orders WHERE masa=" + txtMasa.Text;

                // SQL bağlantısı ve komut nesneleri
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);

                // Veri Okuma
                MySqlDataReader rd = command.ExecuteReader();
                if (rd.Read())
                {
                    lblMasa.Text = rd[3].ToString();
                }
                rd.Close();
                // Veri deposu (DataTable) oluşturun
                DataTable dataTable = new DataTable();

                // Verileri SQL sorgusuyla veritabanından alın
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dataTable);

                // DataGridView'e veri deposunu bağlayın
                dataGridView1.DataSource = dataTable;
                connection.Close();
                MySqlConnection con = new MySqlConnection(connectionString);
                con.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT SUM(fiyat) from orders WHERE masa="+txtMasa.Text, con);
                MySqlDataReader rdd = cmd.ExecuteReader();
                if (rdd.Read())
                {
                    lblUcret.Text = rdd[0].ToString() + " TL";
                }
                rdd.Close();
                con.Close();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                lblSiparisID.Text = selectedRow.Cells[0].Value.ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                // Veritabanına silme sorgusu
                connection.Open();
                string sql = "DELETE FROM orders WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", lblSiparisID.Text);
                // Sorguyu çalıştırma
                cmd.ExecuteNonQuery();
                MessageBox.Show("Ürün başarıyla silindi!", "SAYGIN POS");
                connection.Close();
                zilkontrol = -1;
                GetOrders();
            }
            catch (Exception exec)
            {
                MessageBox.Show("HATA : " + exec.Message);
            }
        }

        private void btnOdemeAl_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                try
                {
                    MySqlConnection connection = new MySqlConnection(connectionString);
                    // Veritabanına ekleme sorgusu
                    connection.Open();
                    string sql = "INSERT INTO log (urun, fiyat) VALUES (@urun, @fiyat)";
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@urun", row.Cells[1].Value.ToString());
                    cmd.Parameters.AddWithValue("@fiyat", row.Cells[2].Value.ToString());

                    // Sorguyu çalıştırma
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception exec)
                {
                    MessageBox.Show("HATA : " + exec.Message);
                }
            }
            MySqlConnection con = new MySqlConnection(connectionString);
            // Veritabanına ekleme sorgusu
            con.Open();
            string com = "DELETE FROM orders WHERE masa=@masa";
            MySqlCommand command = new MySqlCommand(com, con);
            command.Parameters.AddWithValue("@masa", lblMasa.Text);
            // Sorguyu çalıştırma
            command.ExecuteNonQuery();
            MessageBox.Show("Ödeme başarı ile gerçekleşti!", "SAYGIN POS");
            con.Close();
            GetOrders();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            pictureBox2.Width++;
            pictureBox2.Height++;
            if (pictureBox2.Height % 2 == 0) { pictureBox2.Location = new Point(pictureBox2.Location.X - 1, pictureBox2.Location.Y - 1); }
            if (pictureBox2.Height >= 26) { timer2.Stop(); pictureBox2.Size = new Size(26, 26); }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            pictureBox2.Width--;
            pictureBox2.Height--;
            if (pictureBox2.Height % 2 == 0) { pictureBox2.Location = new Point(pictureBox2.Location.X + 1, pictureBox2.Location.Y + 1); }
            if (pictureBox2.Height <= 20) { timer3.Stop(); pictureBox2.Size = new Size(20, 20); }
        }


        private void timer4_Tick(object sender, EventArgs e)
        {
            pictureBox3.Width++;
            pictureBox3.Height++;
            if (pictureBox3.Height % 2 == 0) { pictureBox3.Location = new Point(pictureBox3.Location.X - 1, pictureBox3.Location.Y - 1); }
            if (pictureBox3.Height >= 26) { timer4.Stop(); pictureBox3.Size = new Size(26, 26); }
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            pictureBox3.Width--;
            pictureBox3.Height--;
            if (pictureBox3.Height % 2 == 0) { pictureBox3.Location = new Point(pictureBox3.Location.X + 1, pictureBox3.Location.Y + 1); }
            if (pictureBox3.Height <= 20) { timer5.Stop(); pictureBox3.Size = new Size(20, 20); }
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            timer5.Stop();
            timer4.Start();
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            timer4.Stop();
            timer5.Start();
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            timer3.Stop();
            timer2.Start();
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            timer2.Stop();
            timer3.Start();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection(connectionString);
            // Veritabanına ekleme sorgusu
            con.Open();
            string com = "UPDATE orders SET durum='GÖNDERİLDİ' WHERE ID=@siparis";
            MySqlCommand command = new MySqlCommand(com, con);
            command.Parameters.AddWithValue("@siparis", lblSiparisID.Text);
            // Sorguyu çalıştırma
            command.ExecuteNonQuery();
            con.Close();
            GetOrders();
        }
    }
}
