using MySql.Data.MySqlClient;
using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            const int HEIGHT = 40;
            this.passBox.AutoSize = false;
            this.passBox.Size = new Size(this.passBox.Size.Width, HEIGHT);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            closeButton.ForeColor = Color.Black;
            Thread.Sleep(999);
            Application.Exit();
        }

        private void closeButton_MouseHover(object sender, EventArgs e)
        {
            topPanel.BackColor = Color.FromArgb(175, 34, 35);
            mainPanel.BackColor = Color.FromArgb(175, 34, 35);
            topPanel.Text = "Don`t.";

            LogInButton.Hide();
            pictureBox1.Hide();
            pictureBox2.Hide();
            logInBox.Hide();
            passBox.Hide();
            regLabel.Hide();

            closeButton.ForeColor = Color.Black;
        }

        private void closeButton_MouseLeave(object sender, EventArgs e)
        {
            topPanel.ForeColor = Color.Black;
            topPanel.BackColor = Color.FromArgb(192, 192, 255);
            topPanel.Text = "Authorization";
            mainPanel.BackColor = Color.Linen;
            
            LogInButton.Show();
            pictureBox1.Show();
            pictureBox2.Show();
            logInBox.Show();
            passBox.Show();
            regLabel.Show();

            closeButton.ForeColor = Color.White;
        }

        Point lastPoint;

        private void mainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void mainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void topPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void topPanel_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            string loginUser = logInBox.Text;
            string passUser = passBox.Text;

            DB db = new DB();
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL AND `password` = @uP", db.getConnection());
            //yay, safety
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passUser;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                this.Hide();
                MainForm mainForm = new MainForm();
                mainForm.Show();
            }
            else
                MessageBox.Show("No login?");

        }

        private void regLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegForm regForm = new RegForm();
            regForm.Show();

        }

        private void regLabel_MouseEnter(object sender, EventArgs e)
        {
            regLabel.Font = new Font(regLabel.Font.Name, 10, FontStyle.Bold | FontStyle.Underline);
        }

        private void regLabel_MouseLeave(object sender, EventArgs e)
        {
            regLabel.Font = new Font(regLabel.Font.Name, 10, FontStyle.Bold);
        }
        int counter = 0;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            counter++;
            if (counter == 1)
                MessageBox.Show("It`s locked.");
            else if (counter == 2)
                MessageBox.Show("Nothing to see here.");
            else if (counter == 3)
                MessageBox.Show("Ah, come on.");
            else if (counter == 1000)
                MessageBox.Show("WHAT ARE YOU?");
            else
                MessageBox.Show("Just stop.");

        }
    }
}
