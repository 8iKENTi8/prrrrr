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

namespace prrrrr
{
    public partial class product : UserControl
    {
        public product()
        {
            InitializeComponent();
            ReloadDB();
            Pain_Column();
        }


        DataTable tab;
        string[] idArr;

        public void Pain_Column()
        {
            idArr = new string[table.Rows.Count];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                idArr[i] = table.Rows[i].Cells[0].Value.ToString();

                table[14, i] = linkCell;
                table[14, i].Style.BackColor = Color.FromArgb(46, 169, 79);
            }

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                table[15, i] = linkCell;
                table[15, i].Style.BackColor = Color.Tomato;
            }
        }

        private void ReloadDB()
        {

            DB dB = new DB();

            tab = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command =
                new MySqlCommand("SELECT *, 'Update','Delete'" +
                "FROM `product`", dB.getConnection());

            adapter.SelectCommand = command;

            adapter.Fill(tab);

            table.DataSource = tab;
            Pain_Column();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReloadDB();
        }

        private bool Check(DataGridViewCellEventArgs e)
        {

            for (int j = 0; j < table.ColumnCount - 2; j++)
            {
                if (table.Rows[e.RowIndex].Cells[j].Value.ToString() == "")// проверяем 3-й столбец на пустые ячейки
                {

                    table[j, e.RowIndex].Style.BackColor = Color.Tomato;
                    MessageBox.Show("Не введена запись: " + e.RowIndex + " " + j);
                    return true;
                }
                else
                    table[j, e.RowIndex].Style.BackColor = Color.White;

            }
            return false;
        }

        private void table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 14)
                {
                    string task = table.Rows[e.RowIndex].Cells[14].Value.ToString();
                    if (task == "Update")
                    {


                        //if (Check(e))
                        //    return;


                        //Вопрос вы точно хотите обновить строку , да или нет?
                        if (MessageBox.Show("Обновить эту строку",
                            "Обновление", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            DB db = new DB();
                            MySqlCommand command = new MySqlCommand("UPDATE `mtkp_tip_72_11`.`product` SET " +
                                "`article` = @ul, `name` = @ul1, " +
                                "`type` = @ul2, `about` = @ul3, `min_price` = @ul4, " +
                                "`height` = @ul5, `width` = @ul6, `length` = @ul7, `weight_unboxed` = @ul8, " +
                                "`weight_boxed` = @ul9, `standart_num` = @ul10 WHERE (`id` = @id);", db.getConnection());

                            command.Parameters.Add("@id", MySqlDbType.Int32).Value = idArr[e.RowIndex];
                            command.Parameters.Add("@ul",  MySqlDbType.Int32).Value = table[1, rowIndex].Value.ToString();
                            command.Parameters.Add("@ul1", MySqlDbType.VarChar).Value = table[2, rowIndex].Value.ToString();
                            command.Parameters.Add("@ul2", MySqlDbType.VarChar).Value = table[3, rowIndex].Value.ToString();
                            command.Parameters.Add("@ul3", MySqlDbType.VarChar).Value = table[4, rowIndex].Value.ToString();
                            command.Parameters.Add("@ul4", MySqlDbType.VarChar).Value = table[6, rowIndex].Value.ToString();
                            command.Parameters.Add("@ul5", MySqlDbType.VarChar).Value = table[7, rowIndex].Value.ToString();
                            command.Parameters.Add("@ul6", MySqlDbType.VarChar).Value = table[8, rowIndex].Value.ToString();
                            command.Parameters.Add("@ul7", MySqlDbType.VarChar).Value = table[9, rowIndex].Value.ToString();
                            command.Parameters.Add("@ul8", MySqlDbType.VarChar).Value = table[10, rowIndex].Value.ToString();
                            command.Parameters.Add("@ul9", MySqlDbType.VarChar).Value = table[11, rowIndex].Value.ToString();
                            command.Parameters.Add("@ul10", MySqlDbType.VarChar).Value = table[13, rowIndex].Value.ToString();


                            db.openConnection();
                            if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Запись была обнавлена"); }

                            db.closeConnection();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                if (e.ColumnIndex == 15)
                {
                    string task = table.Rows[e.RowIndex].Cells[15].Value.ToString();
                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Удалить эту строку",
                            "Удаление", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;

                            DB db = new DB();
                            MySqlCommand command = new MySqlCommand("DELETE FROM `product`" +
                                " WHERE product.id = @ul ", db.getConnection());
                            command.Parameters.Add("@ul", MySqlDbType.VarChar).Value = table[0, rowIndex].Value.ToString();

                            table.Rows.RemoveAt(rowIndex);

                            db.openConnection();
                            if (command.ExecuteNonQuery() == 1) { MessageBox.Show("Запись  была удалена"); }

                            db.closeConnection();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new add_product().ShowDialog();
        }
    }
}
