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
                cmd.CommandText = "SELECT TOP 1 * FROM " + Table +
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
                    "\r\nPRIMARY KEY(idFunction));";
                cmd.ExecuteNonQuery();
            }
        }

        internal override void CreateGeneralFunctions()
        {
            using (DbConnection conn = Connect())
            {
                DbCommand cmd = conn.CreateCommand();
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO GeneralFunctions (idFunction, shortDesc, notes) VALUES " +
                    "(2, 'tabella per le funzioni generali', 'molto utile');";
                cmd.ExecuteNonQuery();

            }
        }

        internal override void UpdateTableGF()
        {
            using (DbConnection conn = Connect())
            {
                DbCommand cmd = conn.CreateCommand();
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE GeneralFunctions SET " +
                    "shortDesc = 'update';";
                cmd.ExecuteNonQuery();
            }
        }

        internal override void DeleteTableGF()
        {
            using (DbConnection conn = Connect())
            {
                DbCommand cmd = conn.CreateCommand();
                cmd = conn.CreateCommand();
                cmd.CommandText = "DROP TABLE GeneralFunctions;";
                cmd.ExecuteNonQuery();
                //cmd.CommandText = "CREATE TABLE GeneralFunctions;";
                //cmd.ExecuteNonQuery();

            }
        }
    }
}
