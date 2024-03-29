﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MultiFaceRec
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            this.AcceptButton = login_btn;
        }

        //Database Connection
        public static class DatabaseConnection
        {
            public static string ConnectionString { get; } = @"Data Source=SITTHIPHONE\SQLEXPRESS;Initial Catalog=Login;Integrated Security=True";
        }

        SqlConnection connect = new SqlConnection(DatabaseConnection.ConnectionString);

        private void login_registerHere_Click(object sender, EventArgs e)
        {
            Signup sForm = new Signup();
            sForm.Show();
            this.Hide();
        }

        private void login_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void login_showPass_CheckedChanged(object sender, EventArgs e)
        {
            if (login_showPass.Checked)
            {
                login_password.PasswordChar = '\0';
            }
            else
            {
                login_password.PasswordChar = '*';
            }
        }

        private void login_btn_Click(object sender, EventArgs e)
        {
            if (login_username.Text == "" || login_password.Text == "")
            {
                MessageBox.Show("ກະລຸນາຕື່ມຂໍ້ມູນໃຫ້ຄົບຖ້ວນ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State != ConnectionState.Open)
                {
                    try
                    {
                        connect.Open();

                        String selectData = "SELECT * FROM admin WHERE username = @username AND password = @pass";
                        using (SqlCommand cmd = new SqlCommand(selectData, connect))
                        {
                            cmd.Parameters.AddWithValue("@username", login_username.Text);
                            cmd.Parameters.AddWithValue("@pass", login_password.Text);
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if (table.Rows.Count >= 1)
                            {
                                MessageBox.Show("ເຂົ້າສູ່ລະບົບສຳເລັດ", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                MainForm mForm = new MainForm();
                                mForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("ຊື່ຜູ້ໃຊ້ ຫຼຶ ລະຫັດຜ່ານບໍ່ຖືກຕ້ອງ", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error Connecting: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void login_password_TextChanged(object sender, EventArgs e)
        {

        }
        private void login_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // โค้ดที่ต้องการให้ทำงานเมื่อกดปุ่ม Enter ใน textbox password
                login_btn_Click(sender, e); // เรียกใช้ event handler ของปุ่ม Login button
            }
        }

    }
}

