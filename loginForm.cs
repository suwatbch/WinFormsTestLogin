using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WinFormTestConnect
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ActiveControl = txtUser;
        }

        private bool CkechLogin()
        {
            if (string.IsNullOrEmpty(txtUser.Text) && string.IsNullOrEmpty(txtPass.Text))
            {
                txtUser.Focus();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (CkechLogin())
            {
                return;
            }

            Connect con = new Connect();
            using (SqlConnection conn = new SqlConnection(con.ConnectToDatabase()))
            {
                try
                {
                    conn.Open();

                    string sql = "SELECT * FROM StaffBranch WHERE Username = @Username AND Password = @Password";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@Username", txtUser.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", txtPass.Text.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("Login สำเร็จ!");

                        mainForm mainForm = new mainForm();
                        this.Hide();
                        mainForm.FormClosed += (s, args) => this.Close();
                        mainForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Username หรือ Password ไม่ถูกต้อง");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error connect: " + ex.Message);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void loginForm_Load(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() =>
            {
                txtUser.Focus();
            }));
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPass.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) 
            {
                btnLogin.PerformClick(); 
                e.SuppressKeyPress = true; 
            }
        }

    }
}
