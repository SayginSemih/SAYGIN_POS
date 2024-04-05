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
    public partial class UrunMenu : Form
    {
        public UrunMenu()
        {
            InitializeComponent();
        }

        public string connectionString = "server=localhost;user=root;database=cafeteria;password=";
        public int catagory;
        public int ID;

        public void GetProduct()
        {
            // SQL sorgusu
            string query = "SELECT id as 'ID',urun as 'ÜRÜN',fiyat as 'FİYAT' FROM product WHERE category=" + catagory;

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
        private void UrunMenu_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            this.Text = "SAYGIN POS - CATAGORY : " + catagory;
            try
            {
                for (int i=5001;i<=5018;i++)
                {
                    if (catagory==i)
                    {
                        GetProduct();
                    }
                }
            }
            catch(Exception exec)
            {
                MessageBox.Show("HATA : " + exec.Message, "SAYGIN POS");
            }
        }


        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                // Veritabanına güncelleme sorgusu
                connection.Open();
                string sql = "UPDATE product SET urun=@urun, fiyat=@fiyat WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@urun", txtUrun.Text);
                cmd.Parameters.AddWithValue("@fiyat", txtFiyati.Text);
                cmd.Parameters.AddWithValue("@id", ID);

                // Sorguyu çalıştırma
                cmd.ExecuteNonQuery();
                MessageBox.Show("Ürün başarıyla güncellendi!", "SAYGIN POS");
                connection.Close();
                GetProduct();
            }
            catch (Exception exec)
            {
                MessageBox.Show("HATA : " + exec.Message);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                ID = Convert.ToInt32(selectedRow.Cells[0].Value);
                txtUrun.Text = selectedRow.Cells[1].Value.ToString();
                txtFiyati.Text = selectedRow.Cells[2].Value.ToString();
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(connectionString);
                // Veritabanına silme sorgusu
                connection.Open();
                string sql = "DELETE FROM product WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", ID);
                // Sorguyu çalıştırma
                cmd.ExecuteNonQuery();
                MessageBox.Show("Ürün başarıyla silindi!", "SAYGIN POS");
                connection.Close();
                GetProduct();
            }
            catch (Exception exec)
            {
                MessageBox.Show("HATA : " + exec.Message);
            }
        }

        private void btnAlternatifSogukIcecekler_Click(object sender, EventArgs e)
        {
            for (int i = 5001; i <= 5018; i++)
            {
                if (catagory == i)
                {
                    try
                    {
                        MySqlConnection connection = new MySqlConnection(connectionString);
                        // Veritabanına ekleme sorgusu
                        connection.Open();
                        string sql = "INSERT INTO product (urun, fiyat, category) VALUES (@urun, @fiyat, @category)";
                        MySqlCommand cmd = new MySqlCommand(sql, connection);
                        cmd.Parameters.AddWithValue("@urun", txtUrun.Text);
                        cmd.Parameters.AddWithValue("@fiyat", txtFiyati.Text);
                        cmd.Parameters.AddWithValue("@category", catagory);

                        // Sorguyu çalıştırma
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Ürün başarıyla eklendi!", "SAYGIN POS");
                        connection.Close();
                        GetProduct();
                    }
                    catch (Exception exec)
                    {
                        MessageBox.Show("HATA : " + exec.Message);
                    }
                }
            }
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

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if (txtUrun.Text == "Ürün") { txtUrun.Text = ""; }
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (txtUrun.Text == "") { txtUrun.Text = "Ürün"; }
        }

        private void txtFiyati_Enter(object sender, EventArgs e)
        {
            if (txtFiyati.Text == "Fiyat") { txtFiyati.Text = ""; }
        }

        private void txtFiyati_Leave(object sender, EventArgs e)
        {
            if (txtFiyati.Text == "") { txtFiyati.Text = "Fiyat"; }
        }
    }
}
