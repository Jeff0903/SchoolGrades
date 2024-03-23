using SchoolGrades.BusinessObjects;
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
        public void T_GeneralFunctions_ACreate()
        {
            Test_Commons.dl.CreateTableGF();
            Test_Commons.dl.CreateGeneralFunctions();
        }
        [Test]
        public void T_GeneralFunctions_BRead()
        {
            object sy;
            sy = Test_Commons.dl.ReadFirstRowFirstField("SchoolYears");
            Console.WriteLine(sy);
            // test of DataLayer Methods that read data from table GeneralFunctions
        }
        [Test]
        public void T_GeneralFunctions_CUpdate()
        {
            Test_Commons.dl.UpdateTableGF();
            // test of DataLayer Methods that update data from table GeneralFunctions

        }
        [Test]
        public void T_GeneralFunctions_Delete()
        {
            Test_Commons.dl.DeleteTableGF();
            // test of DataLayer Methods that delete data from table GeneralFunctions
        }
    }
}

