using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CSharp_DATACONNECT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=LAB1-MAY13\MISASME2022;Initial Catalog=QuanLyThongTin;Integrated Security=True;Encrypt=False");
        private void openCon()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }
        private void closeCon()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        private Boolean Exe(string cmd)
        {
            openCon();
            Boolean check;
            try
            {
                SqlCommand sc = new SqlCommand(cmd, con);
                sc.ExecuteNonQuery();
                check = true;
            }
            catch (Exception)
            {
                check = false;
              
            }
            closeCon();
            return check;
        }
        private DataTable Red(string cmd)
        {
            openCon();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand sc = new SqlCommand(cmd, con);
                SqlDataAdapter sda = new SqlDataAdapter(sc);
                sda.Fill(dt);
            }
            catch (Exception)
            {
                dt = null;
                throw;
            }
            closeCon();
            return dt;
        }
        private void load()
        {
            DataTable dt = Red("SELECT * FROM QuanLyThongTin");
            if (dt != null)
            {
                dataGridView1.DataSource = dt;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            load(); 
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            txtmaten.ResetText();
            txthoten.ResetText();
            txtnamsinh.ResetText();
            txtquequan.ResetText();
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            Exe("INSERT INTO QuanLyThongtin(maTen, hoTen, namsinh, quequan) values (N'" + txtmaten.Text + "',N'" + txthoten.Text + "',N'" + txtnamsinh.Text + "',N'" + txtquequan.Text + "')");
            load();
        }

        private void btntrove_Click(object sender, EventArgs e)
        {
            load();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            Exe("UPDATE Quanlythongtin SET maTen = N'"+ txtmaten.Text + "', hoTen = N'"+ txthoten.Text + "', namsinh = N'"+ txtnamsinh.Text + "',quequan = N'"+ txtquequan.Text +"' WHERE maTen ='"+txtmaten.Text+ "')");
            load();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtmaten.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txthoten.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtnamsinh.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtquequan.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            Exe("DELETE FROM Quanlythongtin WHERE maTen ='" + txtmaten.Text + "'");
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            DataTable dt = Red("SELECT * FROM Quanlythongtin WHERE maTen ='" + txttentimkiem.Text + "'");
            if (dt != null)
            {
                dataGridView1.DataSource = dt;  
            }
        }
    }
}
