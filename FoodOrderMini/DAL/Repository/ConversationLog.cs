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
    public class ConversationLog
    {
        public static String connectionstring = ConfigurationManager.ConnectionStrings["FinalOrderLog"].ConnectionString;
        public static void ConversationLogg(string UserName, string UserRequest,string ConversationData,DateTime LoggingTime,string Channel)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("ConersationLog", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@action", SqlDbType.NVarChar, 10).Value = "INSERT";
                    sqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 1000).Value = UserName;
                    sqlCommand.Parameters.Add("@UserRequest", SqlDbType.NVarChar, 4000).Value = UserRequest;

                    sqlCommand.Parameters.Add("@ConversationData", SqlDbType.NVarChar, 4000).Value = ConversationData;
                    sqlCommand.Parameters.Add("@LoggingTime", SqlDbType.DateTime).Value = LoggingTime;
                    sqlCommand.Parameters.Add("@Channel", SqlDbType.NVarChar, 100).Value = Channel;


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