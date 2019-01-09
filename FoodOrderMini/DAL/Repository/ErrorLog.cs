using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ErrorLog
    {
        public static String connectionstring = ConfigurationManager.ConnectionStrings["FinalOrderLog"].ConnectionString;
        public static void errorloginfo(string ErrorMessage, DateTime ErrorTime)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("OrderErrorlogproc", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@action", SqlDbType.NVarChar, 10).Value = "INSERT";
                    sqlCommand.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar, 4000).Value = ErrorMessage;
                    sqlCommand.Parameters.Add("@ErrorTime", SqlDbType.DateTime).Value = ErrorTime;
                    sqlCommand.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
