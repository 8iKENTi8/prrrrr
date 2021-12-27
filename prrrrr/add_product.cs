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
using MySql.Data.MySqlClient;

namespace prrrrr
{
    public partial class add_product : MetroForm
    {
        public add_product()
        {
            InitializeComponent();
        }

        MySqlDataAdapter adapter;

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            list.Add(maskedTextBox1.Text);
            list.Add(maskedTextBox2.Text);
            list.Add(maskedTextBox3.Text);
            list.Add(maskedTextBox4.Text);
            list.Add(maskedTextBox5.Text);
            list.Add(maskedTextBox6.Text);
        }
    }
}
