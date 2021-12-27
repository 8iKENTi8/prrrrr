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
    public partial class Sign_in : MetroForm
    {
        public Sign_in()
        {
            InitializeComponent();
            maskedTextBox5.UseSystemPasswordChar = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sign_up sign_up = new Sign_up();
            this.Hide();
            sign_up.Show();
        }

        // Занесение данных в бд
        private void RegUser()
        {
            DB dB = new DB();

            MySqlCommand command =
                new MySqlCommand("INSERT INTO `mtkp_tip_72_11`.`user` (`first_name`, `middle_name`, `last_name`, `role_id`, `login`, `password`) " +
                "VALUES (@ul, @ul2, @ul3, '3', @ul4, @ul5);",
                dB.getConnection());

            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = maskedTextBox1.Text;
            command.Parameters.Add("@ul2", MySqlDbType.VarChar).Value = maskedTextBox2.Text;
            command.Parameters.Add("@ul3", MySqlDbType.VarChar).Value = maskedTextBox3.Text;
            command.Parameters.Add("@ul4", MySqlDbType.VarChar).Value = maskedTextBox4.Text;
            command.Parameters.Add("@ul5", MySqlDbType.VarChar).Value = maskedTextBox5.Text;

            dB.openConnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Аккаунт был создан!");
            else
                MessageBox.Show("Аккаунт не был создан!");

            dB.closeConnection();

        }


        // Проверка существует ли ползователь
        public Boolean isUserExists()
        {
            DB dB = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT * FROM user where user.login=@ul",
                dB.getConnection());

            command.Parameters.Add("@ul",
                MySqlDbType.VarChar).Value = maskedTextBox4.Text;

            adapter.SelectCommand = command;

            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже есть, введите другой");
                return true;
            }
            else
                return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fs = maskedTextBox1.Text.Trim(),
                  name = maskedTextBox2.Text.Trim(),
                  last = maskedTextBox3.Text.Trim(),
                  log = maskedTextBox4.Text.Trim(),
                  pass = maskedTextBox5.Text.Trim();


            // Список для проверки данных
            List<string> list = new List<string>();
            list.Add(fs);
            list.Add(name);
            list.Add(last);
            list.Add(log);
            list.Add(pass);

            if (DB.ChecBox(list))
            {
                MessageBox.Show("Не все данные введены");
                return;
            }


            if (log.Length < 4 || log.Length > 20)
            {
                MessageBox.Show("Логин введен неверно!");
                return;
            }

            if (log.Contains("@") || log.Contains("."))
            {
                MessageBox.Show("Логин содержит некорректные символы");
                return;
            }

            if (pass.Length < 5 && pass.Length > 20)
            {
                MessageBox.Show("Пароль введен неверно!");
                return;
            }

            


            if (isUserExists())
                return;

            RegUser();

            this.Hide();
            Sign_up sign = new Sign_up();
            sign.Show();
        }








        //    public partial class Dolznocti : UserControl
        //    {
        //        public Dolznocti()
        //        {
        //            InitializeComponent();
        //            if (DB.user_role == 1)
        //            {
        //                button1.Visible = false;
        //                button2.Visible = false;
        //            }
        //        }


        //        DataTable tab;
        //        string[] idArr;

        //        private void ReloadDB()
        //        {

        //            DB dB = new DB();

        //            tab = new DataTable();

        //            MySqlDataAdapter adapter = new MySqlDataAdapter();

        //            MySqlCommand command =
        //                new MySqlCommand("SELECT *, 'Update','Delete'" +
        //                "FROM `должность`", dB.getConnection());

        //            adapter.SelectCommand = command;

        //            adapter.Fill(tab);

        //            table.DataSource = tab;
        //            Pain_Column();

        //        }

        //        private void button1_Click(object sender, EventArgs e)
        //        {
        //            new AddDol().ShowDialog();
        //        }

        //        private void button2_Click(object sender, EventArgs e)
        //        {
        //            ReloadDB();
        //        }

        //        public void Pain_Column()
        //        {
        //            idArr = new string[table.Rows.Count];

        //            for (int i = 0; i < table.Rows.Count; i++)
        //            {
        //                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

        //                idArr[i] = table.Rows[i].Cells[0].Value.ToString();

        //                table[3, i] = linkCell;
        //                table[3, i].Style.BackColor = Color.FromArgb(46, 169, 79);
        //            }

        //            for (int i = 0; i < table.Rows.Count; i++)
        //            {
        //                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

        //                table[4, i] = linkCell;
        //                table[4, i].Style.BackColor = Color.Tomato;
        //            }
        //        }

        //        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        //        {
        //            if (e.KeyChar == (char)13)
        //            {
        //                DataView data = tab.DefaultView;
        //                data.RowFilter = string.Format("`Название` like '%{0}%'", txtSearch.Text);
        //                table.DataSource = data.ToTable();

        //                Pain_Column();
        //            }

        //            if (txtSearch.Text == "")
        //            {
        //                Pain_Column();
        //            }
        //        }

        //        private bool Check(DataGridViewCellEventArgs e)
        //        {

        //            for (int j = 0; j < table.ColumnCount - 2; j++)
        //            {
        //                if (table.Rows[e.RowIndex].Cells[j].Value.ToString() == "")// проверяем 3-й столбец на пустые ячейки
        //                {

        //                    table[j, e.RowIndex].Style.BackColor = Color.Tomato;
        //                    MessageBox.Show("Не введена запись: " + e.RowIndex + " " + j);
        //                    return true;
        //                }
        //                else
        //                    table[j, e.RowIndex].Style.BackColor = Color.White;

        //            }
        //            return false;
        //        }

        //        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //        {
        //            try
        //            {
        //                if (e.ColumnIndex == 3)
        //                {
        //                    string task = table.Rows[e.RowIndex].Cells[3].Value.ToString();
        //                    if (task == "Update")
        //                    {

        //                        if (Check(e))
        //                            return;

        //                        //Вопрос вы точно хотите обновить строку , да или нет?
        //                        if (MessageBox.Show("Обновить эту строку",
        //                            "Обновление", MessageBoxButtons.YesNo,
        //                            MessageBoxIcon.Question) == DialogResult.Yes)
        //                        {
        //                            int rowIndex = e.RowIndex;

        //                            DB db = new DB();
        //                            MySqlCommand command = new MySqlCommand("UPDATE `должность` SET `id_do` = @ul," +
        //                                " `Название` = @ul1, " +
        //                                "`Зарплата` = @ul2 WHERE `должность`.`id_do` = @id", db.getConnection());

        //                            command.Parameters.Add("@id", MySqlDbType.Int32).Value = idArr[e.RowIndex];
        //                            command.Parameters.Add("@ul", MySqlDbType.Int32).Value = table[0, rowIndex].Value.ToString();
        //                            command.Parameters.Add("@ul1", MySqlDbType.VarChar).Value = table[1, rowIndex].Value.ToString();
        //                            command.Parameters.Add("@ul2", MySqlDbType.VarChar).Value = table[2, rowIndex].Value.ToString();


        //                            db.openConnection();
        //                            if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Запись была обнавлена"); }

        //                            db.closeConnection();
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {

        //                MessageBox.Show(ex.Message, "Ошибка!",
        //                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }

        //            try
        //            {
        //                if (e.ColumnIndex == 4)
        //                {
        //                    string task = table.Rows[e.RowIndex].Cells[4].Value.ToString();
        //                    if (task == "Delete")
        //                    {
        //                        if (MessageBox.Show("Удалить эту строку",
        //                            "Удаление", MessageBoxButtons.YesNo,
        //                            MessageBoxIcon.Question) == DialogResult.Yes)
        //                        {
        //                            int rowIndex = e.RowIndex;

        //                            DB db = new DB();
        //                            MySqlCommand command = new MySqlCommand("DELETE FROM `должность`" +
        //                                " WHERE `должность`.`id_do` = @ul ", db.getConnection());
        //                            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();

        //                            table.Rows.RemoveAt(rowIndex);

        //                            db.openConnection();
        //                            if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Запись  была удалена"); }

        //                            db.closeConnection();
        //                        }
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {

        //                MessageBox.Show(ex.Message, "Ошибка!",
        //                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }

        //        private void Dolznocti_Load(object sender, EventArgs e)
        //        {
        //            ReloadDB();
        //        }
        //    }
        //}

    }

      
    }

//DataTable table;
//MySqlCommand command;
//MySqlDataAdapter adapter;

//private void pictureBox1_Click(object sender, EventArgs e)
//{

//}

//private void guna2TextBox1_TextChanged(object sender, EventArgs e)
//{

//}

//private void label8_Click(object sender, EventArgs e)
//{
//    Application.Exit();
//}

//private void button2_Click(object sender, EventArgs e)
//{
//    string login = guna2TextBox1.Text.Trim(),
//           pass = guna2TextBox2.Text.Trim();

//    if (login.Length < 4)
//    {
//        MessageBox.Show("Логин введен неверно!");
//        return;
//    }

//    if (pass.Length < 4)
//    {
//        MessageBox.Show("Пароль введен неверно!");
//        return;
//    }

//    if (isUserExists(login, pass))
//    {
//        this.Hide();
//        Form1 form = new Form1();
//        form.Show();
//        return;
//    }
//    else
//        MessageBox.Show("Пользователя не существует");
//}

//// Проверка есть ли пользователь в бд
//public Boolean isUserExists(string log, string pass)
//{
//    if (log == null || pass == null)
//        throw new ArgumentNullException("Введеные данные не могут быть пустые");

//    DB dB = new DB();

//    table = new DataTable();

//    adapter = new MySqlDataAdapter();

//    command =
//       new MySqlCommand("SELECT * FROM `сотрудник` WHERE `Имя` = @ul AND" +
//       "`pass`= @up", dB.getConnection());

//    command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = log;
//    command.Parameters.Add("@up", MySqlDbType.VarChar).Value = pass;

//    adapter.SelectCommand = command;

//    adapter.Fill(table);



//    if (table.Rows.Count > 0)
//    {
//        DB.user_role = Int32.Parse(table.Rows[0][5].ToString());
//        DB.user_name = table.Rows[0][2].ToString();

//        return true;
//    }

//    table = new DataTable();

//    adapter = new MySqlDataAdapter();

//    command =
//       new MySqlCommand("SELECT * FROM `клиент` WHERE `Компания` = @ul AND" +
//       "`pass`= @up", dB.getConnection());

//    command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = log;
//    command.Parameters.Add("@up", MySqlDbType.VarChar).Value = pass;

//    adapter.SelectCommand = command;

//    adapter.Fill(table);



//    if (table.Rows.Count > 0)
//    {
//        DB.user_role = -1;
//        DB.user_name = table.Rows[0][1].ToString();
//        DB.user_id = Int32.Parse(table.Rows[0][0].ToString());

//        return true;
//    }

//    return false;

//}