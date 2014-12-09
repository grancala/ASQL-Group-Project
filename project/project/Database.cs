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
        /// Creates a user given a username
        /// </summary>
        /// <param name="UserName">Username</param>
        /// <returns>True is successful</returns>
        public static bool CreateUser(string UserName)
        {
            bool results = false;
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                try
                {
                    sqlConn.Open();

                    // Perform a count on the table.
                    SqlCommand command = new SqlCommand("CreateUser", sqlConn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userName", UserName);
                    command.Parameters.Add("@rowCount", SqlDbType.Int);
                    command.Parameters["@rowCount"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    int count = (int)command.Parameters["@rowCount"].Value;
                    if (count > 0)
                    {
                        results = true;
                    }
                }
                catch (Exception e)
                {
                    StringBuilder builder = new StringBuilder();
                    Helper.BuildErrorMessage(e, ref builder);
                    Logging.LogError(UserName, builder.ToString());
                    results = false;
                }
                finally
                {
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        sqlConn.Close();
                    }
                }
            }
            return results;
        }

        /// <summary>
        /// Updates a user's password
        /// </summary>
        /// <param name="UserName">User to update</param>
        /// <param name="oldPass">Old password</param>
        /// <param name="newPass">New password</param>
        /// <returns>true if successful</returns>
        public static bool UpdateUser (string UserName, string oldPass, string newPass)
        {
            bool results = false;
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                try
                {
                    sqlConn.Open();

                    // Perform a count on the table.
                    SqlCommand command = new SqlCommand("UpdateUser", sqlConn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userName", UserName);
                    command.Parameters.AddWithValue("@oldPass", oldPass);
                    command.Parameters.AddWithValue("@newPass", newPass);
                    command.Parameters.Add("@rowCount", SqlDbType.Int);
                    command.Parameters["@rowCount"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    int count = (int)command.Parameters["@rowCount"].Value;
                    if (count > 0)
                    {
                        results = true;
                    }
                }
                catch (Exception e)
                {
                    StringBuilder builder = new StringBuilder();
                    Helper.BuildErrorMessage(e, ref builder);
                    Logging.LogError(UserName, builder.ToString());
                    results = false;
                }
                finally
                {
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        sqlConn.Close();
                    }
                }
            }
            return results;
        }

        /// <summary>
        /// Removes a user given a username
        /// </summary>
        /// <param name="UserName">User to remove</param>
        /// <param name="Password">Password to ensure user</param>
        /// <returns>True if successful</returns>
        public static bool DropUser(string UserName, string Password)
        {
            bool results = false;
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                try
                {
                    sqlConn.Open();

                    // Perform a count on the table.
                    SqlCommand command = new SqlCommand("DropUser", sqlConn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userName", UserName);
                    command.Parameters.AddWithValue("@userPass", Password);
                    command.Parameters.Add("@rowCount", SqlDbType.Int);
                    command.Parameters["@rowCount"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    int count = (int)command.Parameters["@rowCount"].Value;
                    if (count > 0)
                    {
                        results = true;
                    }
                }
                catch (Exception e)
                {
                    StringBuilder builder = new StringBuilder();
                    Helper.BuildErrorMessage(e, ref builder);
                    Logging.LogError(UserName, builder.ToString());
                    results = false;
                }
                finally
                {
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        sqlConn.Close();
                    }
                }
            }
            return results;
        }


        /// <summary>
        /// Creates a table using the standard structure
        /// Calls stored proc CreateTable
        /// </summary>
        /// <param name="UserName">User requesting action</param>
        /// <param name="password">User password</param>
        /// <param name="overwrite">Is data allowed to be over written</param>
        /// <returns>True if successful</returns>
        public static bool CreateTable (string UserName, string password, bool overwrite)
        {
            bool result = false;
            // send SQL
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                try
                {
                    sqlConn.Open();

                    // Perform a count on the table.
                    SqlCommand command = new SqlCommand("CreateTable", sqlConn);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userName", UserName);
                    command.Parameters.AddWithValue("@userPassword", password);
                    command.Parameters.AddWithValue("@overWrite", overwrite);
                    command.Parameters.Add("@successful", SqlDbType.Bit);
                    command.Parameters["@successful"].Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    result = (bool)command.Parameters["@successful"].Value;
                }
                catch (Exception e)
                {
                    StringBuilder builder = new StringBuilder();
                    Helper.BuildErrorMessage(e, ref builder);
                    Logging.LogError(UserName, builder.ToString());
                    result = false;
                }
                finally
                {
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        sqlConn.Close();
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Determines if the table has data or not
        /// Calls stored proc TableExists
        /// </summary>
        /// <param name="UserName">User requesting action</param>
        /// <param name="password">Users password</param>
        /// <returns>True if data is there</returns>
        public static long TableHasContents (string UserName, string password)
        {
            long count = 0;

            // send SQL
            using (SqlConnection sqlConn = new SqlConnection(ConnectionString))
            {
                try
                {
                    sqlConn.Open();

                    // Perform a count on the table.
                    SqlCommand commandRowCount = new SqlCommand("TableExists", sqlConn);
                    commandRowCount.CommandType = CommandType.StoredProcedure;
                    commandRowCount.Parameters.AddWithValue("@userName", UserName);
                    commandRowCount.Parameters.AddWithValue("@userPassword", password);
                    commandRowCount.Parameters.Add("@dataCount", SqlDbType.BigInt);
                    commandRowCount.Parameters["@dataCount"].Direction = ParameterDirection.Output;
                    commandRowCount.ExecuteNonQuery();
                    count = (long)commandRowCount.Parameters["@dataCount"].Value;
                }
                catch (Exception e)
                {
                    StringBuilder builder = new StringBuilder();
                    Helper.BuildErrorMessage(e, ref builder);
                    Logging.LogError(UserName, builder.ToString());
                    count = -1;
                }
                finally
                {
                    if (sqlConn.State == ConnectionState.Open)
                    {
                        sqlConn.Close();
                    }
                }
            }
            return count;
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

                    string deleteSQL = "DELETE FROM " + table + ";";
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
