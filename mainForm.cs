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
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(MainForm_KeyDown);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                ออกจากระบบToolStripMenuItem_Click(sender, EventArgs.Empty);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            Connect con = new Connect();
            using (SqlConnection conn = new SqlConnection(con.ConnectToDatabase()))
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM Bank";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataDgv.DataSource = dt;
                    
                    dataDgv.Columns[0].HeaderText = "ID";
                    dataDgv.Columns[1].HeaderText = "ชื่อไทย";
                    dataDgv.Columns[2].HeaderText = "ชื่ออังกฤษ";
                    dataDgv.Columns[3].HeaderText = "ชื่อย่อ";
                    // dataDgv.Columns[4].HeaderText = "โลโก้";
                    
                    // MessageBox.Show("เชื่อมต่อและดึงข้อมูลสำเร็จ!", "Success");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error connect: " + ex.Message);
                }
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            dataDgv.Columns.Clear();
        }

        private void dataDgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void mainForm_Load(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }

        private void ออกจากระบบToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "คุณแน่ใจหรือไม่ว่าต้องการออกจากระบบ?", 
                "ยืนยันการออกจากระบบ",                  
                MessageBoxButtons.YesNo,                 
                MessageBoxIcon.Question                
            );

            if (result == DialogResult.Yes)
            {
                MessageBox.Show("คุณได้ออกจากระบบแล้ว");
                loginForm loginForm = new loginForm();
                loginForm.Show();
                Application.Restart();
            }
        }

        private void ปดโปรแกรมToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
