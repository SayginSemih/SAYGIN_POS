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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        MySqlConnection con = new MySqlConnection("server=localhost;user=root;database=cafeteria;password=");

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string sql = "SELECT COUNT(*) FROM users WHERE username = @username AND passwd = @password";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPasswd.Text);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    MainMenu mn = new MainMenu();
                    this.Hide();
                    mn.Show();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre yanlış!", "SAYGIN POS");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if(txtUsername.Text == "Kullanıcı Adı") { txtUsername.Text = ""; }
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if (txtUsername.Text == "") { txtUsername.Text = "Kullanıcı Adı"; }
        }

        private void txtPasswd_Enter(object sender, EventArgs e)
        {
            if( txtPasswd.Text == "Şifre")
            {
                txtPasswd.Text = "";
                txtPasswd.isPassword = true;
            }
        }

        private void txtPasswd_Leave(object sender, EventArgs e)
        {
            if (txtPasswd.Text == "")
            {
                txtPasswd.Text = "Şifre";
                txtPasswd.isPassword = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox2.Width++;
            pictureBox2.Height++;
            if (pictureBox2.Height % 2 == 0) { pictureBox2.Location = new Point(pictureBox2.Location.X - 1, pictureBox2.Location.Y - 1); }
            if(pictureBox2.Height >= 26) { timer1.Stop(); pictureBox2.Size = new Size(26, 26); }
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
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
