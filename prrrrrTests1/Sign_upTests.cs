using Microsoft.VisualStudio.TestTools.UnitTesting;
using prrrrr;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prrrrr.Tests
{
    [TestClass()]
    public class Sign_upTests
    {
        [TestMethod()]
        public void TestInputDataFalse()
        {
            int x = 10;

            List<string> list = new List<string>();
            list.Add("");
            list.Add("safsafasf");
            list.Add("f");
            list.Add(x.ToString());

            //act
            Assert.AreEqual(DB.ChecBox(list), true);
            Debug.WriteLine("Проверка прошла успешно,должны быть веддены все данные");
        }

        [TestMethod()]
        public void isUserExistsTestToBeUserInDb()
        {
            string log = "vova", pass = "12345";

            Sign_up sign_In = new Sign_up();

            Assert.AreEqual(sign_In.isUserExists(log, pass), true);
            Debug.WriteLine("Такой пользователь существует");
        }


        [TestMethod()]
        public void isUserExistsTestNotUserInDb()
        {
            string log = "Такого пользователя нет", pass = "afaf";

            Sign_up sign_In = new Sign_up();

            Assert.AreEqual(sign_In.isUserExists(log, pass), false);
            Debug.WriteLine("Такого пользователя нет");
        }


        [ExpectedException(typeof(ArgumentNullException), "Exception wan not thrown")]
        [TestMethod()]
        public void isUserExistsTestNull()
        {
            string log = null, pass = null;

            Sign_up sign_Up = new Sign_up();

           

            Assert.AreEqual(sign_Up.isUserExists(log, pass), false);
            Debug.WriteLine("Проверка прошла успешно,передаваем обьект не может быть пустой");

        }
        }
    }