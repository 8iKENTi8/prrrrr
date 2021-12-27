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
    public partial class Sign_up : MetroForm
    {
        public Sign_up()
        {
            InitializeComponent();

            maskedTextBox3.UseSystemPasswordChar = true;
        }
        DataTable table;
        MySqlDataAdapter adapter;
        MySqlCommand command;


        private void button2_Click(object sender, EventArgs e)
        {
            Sign_in f = new Sign_in();
            this.Hide();
            f.Show();
        }

        public Boolean isUserExists(string log, string pass)
        {
            if (log == null || pass == null)
                throw new ArgumentNullException("Введеные данные не могут быть пустые");

            DB dB = new DB();

            table = new DataTable();

            adapter = new MySqlDataAdapter();

            command =
               new MySqlCommand("SELECT * FROM user, user_role " +
               "where user.login = @ul and user.password=@up and " +
               "user_role.id = user.role_id and user_role.id=1;", dB.getConnection());

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = log;
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = pass;

            adapter.SelectCommand = command;

            adapter.Fill(table);



            if (table.Rows.Count > 0)
            {
                DB.user_role = Int32.Parse(table.Rows[0][4].ToString());
                DB.user_name = table.Rows[0][2].ToString();

                return true;
            }

            table = new DataTable();

            adapter = new MySqlDataAdapter();

            command =
               new MySqlCommand("SELECT * FROM user, user_role " +
               "where user.login = @ul and user.password=@up and " +
               "user_role.id = user.role_id and user_role.id=2;", dB.getConnection());

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = log;
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = pass;

            adapter.SelectCommand = command;

            adapter.Fill(table);



            if (table.Rows.Count > 0)
            {
                DB.user_role = Int32.Parse(table.Rows[0][4].ToString());
                DB.user_name = table.Rows[0][2].ToString();

                return true;
            }

            table = new DataTable();

            adapter = new MySqlDataAdapter();

            command =
               new MySqlCommand("SELECT * FROM user, user_role " +
               "where user.login = @ul and user.password=@up and " +
               "user_role.id = user.role_id and user_role.id=3;", dB.getConnection());

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = log;
            command.Parameters.Add("@up", MySqlDbType.VarChar).Value = pass;

            adapter.SelectCommand = command;

            adapter.Fill(table);



            if (table.Rows.Count > 0)
            {
                DB.user_role = Int32.Parse(table.Rows[0][4].ToString());
                DB.user_name = table.Rows[0][2].ToString();

                return true;
            }


            return false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = maskedTextBox2.Text.Trim(),
           pass = maskedTextBox3.Text.Trim();

            List<string> list = new List<string>();
            list.Add(login);
            list.Add(pass);

           if(DB.ChecBox(list))
            {
                MessageBox.Show("Не все данные введены");
                return;
            }

            if (isUserExists(login, pass))
            {
                this.Hide();
                Main form = new Main();
                form.Show();
                return;
            }
            else
                MessageBox.Show("Пользователя не существует");
        }
    }
}
