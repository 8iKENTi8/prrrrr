using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prrrrr
{
    public partial class DB
    {

        public static int user_role, user_id;
        public static string user_name;
        //Строка подключения к бд

        MySqlConnection connection = new MySqlConnection("server=95.131.149.21;" +
            "port=3306;username=mtkp_tip_72_11;password=14006367;database=mtkp_tip_72_11");

        //Открывает соединение
        public void openConnection()
        {
            //Если соединение закрыто, то открываем
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        //Закрывает соединение
        public void closeConnection()
        {
            //Если соединение открыто, то закрывавем
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        //Получаем соединение
        public MySqlConnection getConnection()
        {
            return connection;
        }

        public static bool ChecBox(List<string> tx)
        {
            if (tx == null)
                throw new ArgumentNullException("Введеные данные не могут быть пустые");

            foreach (var i in tx)
                if (i == "")
                    return true;
            return false;
        }

    } 
}
