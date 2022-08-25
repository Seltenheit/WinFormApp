using MySql.Data.MySqlClient;
using System;
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
    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();

            nameField.Text = "Your name";
            nameField.ForeColor = Color.Gray;
            surnameField.Text = "Your surname";
            surnameField.ForeColor = Color.Gray;
            loginField.Text = "Your login";
            loginField.ForeColor = Color.Gray;
            passField.Text = "Your password";
            passField.ForeColor = Color.Gray;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
           Application.Exit();
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


        private void nameField_Enter(object sender, EventArgs e)
        {
            if (nameField.Text == "Your name")
            {
                nameField.Text = "";
                nameField.ForeColor = Color.Black;
            }
        }

        private void nameField_Leave(object sender, EventArgs e)
        {
            if (nameField.Text == "")
            {
                nameField.Text = "Your name";
                nameField.ForeColor = Color.Gray;
            }
        }


        private void surnameField_Enter(object sender, EventArgs e)
        {
            if (surnameField.Text == "Your surname")
            {
                surnameField.Text = "";
                surnameField.ForeColor = Color.Black;
            }
        }

        private void surnameField_Leave(object sender, EventArgs e)
        {
            if (surnameField.Text == "")
            {
                surnameField.Text = "Your surname";
                surnameField.ForeColor = Color.Gray;
            }

        }


        private void loginField_Enter(object sender, EventArgs e)
        {
            if (loginField.Text == "Your login")
            {
                loginField.Text = "";
                loginField.ForeColor = Color.Black;
            }
           
        }

        private void loginField_Leave(object sender, EventArgs e)
        {
            if (loginField.Text == "")
            {
                loginField.Text = "Your login";
                loginField.ForeColor = Color.Gray;
            }
        }


        private void passField_Enter(object sender, EventArgs e)
        {
            if (passField.Text == "Your password")
            {
                passField.UseSystemPasswordChar = true;
                passField.Text = "";
                passField.ForeColor = Color.Black;
            }
        }

        private void passField_Leave(object sender, EventArgs e)
        {
            if (passField.Text == "")
            {
                passField.UseSystemPasswordChar = false;
                passField.Text = "Your password";
                passField.ForeColor = Color.Gray;
            }
        }


        private void signInButton_Click(object sender, EventArgs e)
        {
            if (!FillCheck())
                return;

            if (IsUserExists())
                return;

            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `password`, `name`, `surname`) VALUES (@login, @pass, @name, @surname)", db.getConnection()); 

            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = loginField.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = passField.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = nameField.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = surnameField.Text;

            //db.openConnection();
            db.getConnection().Open();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("The account was created.");
            else
                MessageBox.Show("Failed to create the account.");

            db.closeConnection();

        }

        public Boolean IsUserExists()
        {
            DB db = new DB();
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL", db.getConnection());
            //yay, safety
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginField.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("This login already exists.");
                return true;
            }
            else
            {
                return false;
            }
                
        }

        private Boolean FillCheck()
        {
            if (nameField.Text == "Your name")
            {
                MessageBox.Show("Enter your name");
                return false;
            }
            else if(surnameField.Text == "Your surname")
            {
                MessageBox.Show("Enter your surname");
                return false;
            }
            else if (loginField.Text == "Your login")
            {
                MessageBox.Show("Enter your login");
                return false;
            }
            else if(passField.Text == "Your password")
            {
                MessageBox.Show("Enter your password");
                return false;
            }
            else
            {
                return true;
            }    

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("It`s locked.");
        }

        private void regLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm logForm = new LoginForm();
            logForm.Show();
        }

        private void regLabel_MouseEnter(object sender, EventArgs e)
        {
            regLabel.Font = new Font(regLabel.Font.Name, 10, FontStyle.Bold | FontStyle.Underline);
        }

        private void regLabel_MouseLeave(object sender, EventArgs e)
        {
            regLabel.Font = new Font(regLabel.Font.Name, 10, FontStyle.Bold);
        }
    }
}
