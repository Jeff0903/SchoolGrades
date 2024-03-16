using System.Data.Common;

namespace SchoolGrades
{
    internal partial class SqlServer_DataLayer : DataLayer
    {
        internal override object ReadFirstRowFirstField(string Table)
        {
            object r;
            using (DbConnection conn = Connect())
            {
                DbCommand cmd = conn.CreateCommand();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM " + Table +
                    " LIMIT 1" +
                    ";";
                r = cmd.ExecuteScalar();
            }
            return r;
        }
        internal override void CreateTableGF()
        {
            using (DbConnection conn = Connect())
            {
                DbCommand cmd = conn.CreateCommand();
                cmd = conn.CreateCommand();
                cmd.CommandText = "CREATE TABLE GeneralFunctions " +
                    "\r\n(idFunction VARCHAR(5) NOT NULL," +
                    "\r\n shortDesc VARCHAR(10) NULL," +
                    "\r\nnotes VARCHAR(255) NULL," +
                    "\r\nPRIMARY KEY(idFunction)) ";
                cmd.ExecuteNonQuery();
       
            }
        }
    }
}
