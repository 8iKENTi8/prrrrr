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
using MetroFramework.Forms;

namespace prrrrr
{
    public partial class Main : MetroForm
    {
        public Main()
        {
            InitializeComponent();
            label2.Text = DB.user_name;

            DB dB = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
               new MySqlCommand("SELECT * FROM user_role where user_role.id = @ul ", dB.getConnection());

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = DB.user_role;
           

            adapter.SelectCommand = command;

            adapter.Fill(table);



            if (table.Rows.Count > 0)
                label1.Text += table.Rows[0][1].ToString();
            else
                MessageBox.Show("Не удалось получить роль");

            product uc = new product();
            addUserControl(uc);
            uc.Pain_Column();


        }

        private void addUserControl(UserControl us)
        {
            us.Dock = DockStyle.Fill;
            panel_container.Controls.Clear();
            panel_container.Controls.Add(us);
            us.BringToFront();
            
        }
    }
}
