using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    /// <summary>
    /// Class to interface with the database specified by a ConnectionString
    /// </summary>
    public static class Database
    {
        public static string ConnectionString = string.Empty;

        /// <summary>
        /// Executes a non query on the database
        /// </summary>
        /// <param name="UserName">User requesting action</param>
        /// <param name="sql">SQL to send</param>
        /// <returns>True if successful</returns>
        private static bool ExecuteNonQuery(string UserName, string sql)
        {
            bool successful = true;
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                try
                {
                    sqlConn.Open();
                    // send sql
                }
                catch (Exception e)
                {
                    StringBuilder builder = new StringBuilder();
                    Helper.BuildErrorMessage(e, ref builder);
                    Logging.LogError(UserName, builder.ToString());
                    successful = false;
                }
                finally
                {
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        sqlConn.Close();
                    }
                }
            }
            return successful;
        }


        /// <summary>
        /// Creates a table using the standard structure
        /// </summary>
        /// <param name="UserName">User requesting action</param>
        /// <param name="TableName">Name for the new table</param>
        /// <returns>True if successful</returns>
        public static bool CreateTable (string UserName, string TableName)
        {
            string sql = "CREATE TABLE " + TableName;
            return ExecuteNonQuery(UserName, sql);
        }


        /// <summary>
        /// Drops the table in the database
        /// </summary>
        /// <param name="UserName">User requesting action</param>
        /// <param name="TableName">Table to drop</param>
        /// <returns>True if successful</returns>
        public static bool DropTable (string UserName, string TableName)
        {
            string sql = "DROP TABLE " + TableName + ";";
            return ExecuteNonQuery(UserName, sql);
        }


        /// <summary>
        /// Determines if the table has data or not
        /// </summary>
        /// <param name="UserName">User requesting action</param>
        /// <param name="TableName">Table to check</param>
        /// <returns>True if data is there</returns>
        public static bool TableHasContents (string UserName, string TableName)
        {
            bool contents = true;
            long count = 0;

            string SQL = "SELECT COUNT(*) FROM " + TableName + ";";

            // send SQL
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                try
                {
                    sqlConn.Open();

                    // Perform a count on the table.
                    SqlCommand commandRowCount = new SqlCommand(SQL, sqlConn);
                    count = System.Convert.ToInt32(commandRowCount.ExecuteScalar());
                }
                catch (Exception e)
                {
                    StringBuilder builder = new StringBuilder();
                    Helper.BuildErrorMessage(e, ref builder);
                    Logging.LogError(UserName, builder.ToString());
                    contents = false;
                }
                finally
                {
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        sqlConn.Close();
                    }
                }
            }
            
            if (count == 0)
            {
                contents = false;
            }
            return contents;
        }


        /// <summary>
        /// Truncates the specified table and copies the data from the insert
        /// </summary>
        /// <param name="UserName">User requesting insert</param>
        /// <param name="table">Tablename to use</param>
        /// <param name="insert">Data to insert</param>
        /// <returns>true if successful</returns>
        public static bool Insert(string UserName, string table, DataTable insert)
        {
            bool successful = true;

            // set transaction
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                SqlTransaction trans = null;
                try
                {
                    sqlConn.Open();
                    trans = sqlConn.BeginTransaction();
                    SqlCommand command;

                    string deleteSQL = "TRUNCATE TABLE " + table + ";";
                    command = new SqlCommand(deleteSQL, sqlConn, trans);
                    command.ExecuteNonQuery();

                    // bulk copy
                    using (SqlBulkCopy blkCpy = new SqlBulkCopy(sqlConn, SqlBulkCopyOptions.Default, trans))
                    {
                        blkCpy.DestinationTableName = table;
                        blkCpy.WriteToServer(insert);
                    }

                    trans.Commit();
                }
                catch (Exception e)
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                    }

                    StringBuilder builder = new StringBuilder();
                    Helper.BuildErrorMessage(e, ref builder);
                    Logging.LogError(UserName, builder.ToString());
                    successful = false;
                }
                finally
                {
                    if(sqlConn.State == ConnectionState.Open)
                    {
                        sqlConn.Close();
                    }
                }
            }

            return successful;
        }
    }
}
