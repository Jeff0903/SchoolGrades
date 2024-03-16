using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitDbTests
{
    internal class T_GeneralFunctions
    {
        [SetUp]
        public void SetUp()
        {
            Test_Commons.SetDataLayer();
        }

        [Test]
        public void T_GeneralFunctions_Create()
        {
            Test_Commons.dl.CreateTableGF();
        }
        [Test]
        public void T_GeneralFunctions_Read()
        {
            //Test_Commons.dl.ReadFirstRowFirstField();
            // test of DataLayer Methods that read data from table GeneralFunctions
        }
        [Test]
        public void T_GeneralFunctions_Update()
        {
            // test of DataLayer Methods that update data from table GeneralFunctions

        }
        [Test]
        public void T_GeneralFunctions_Delete()
        {
            // test of DataLayer Methods that delete data from table GeneralFunctions
        }
    }
}

