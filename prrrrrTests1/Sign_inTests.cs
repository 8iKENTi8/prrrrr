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
    public class Sign_inTests
    {
        [ExpectedException(typeof(ArgumentNullException), "Exception wan not thrown")]
        [TestMethod()]

        public void TestInputDataNull()
        {

            List<string> list = null;
            Assert.AreEqual(DB.ChecBox(list), true);
            Debug.WriteLine("Проверка прошла успешно,передаваем обьект не может быть пустой");
        }


        [TestMethod()]
        public void TestInputDataFalseReg()
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
    }
}