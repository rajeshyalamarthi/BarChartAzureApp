using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALRepository
{
    public  class Logging
    {
        public static String connectionstring = ConfigurationManager.ConnectionStrings["LccChatBot"].ConnectionString;
        // method to log all the errors in the Database
        public static void errorloginfo(string ErrorMessage, DateTime ErrorTime)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("OrderErrorlogproc", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar, 4000).Value = ErrorMessage;
                    sqlCommand.Parameters.Add("@ErrorTime", SqlDbType.DateTime).Value = ErrorTime;
                    sqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                errorloginfo(ex.Message, DateTime.Now);
            }
        }

        // method to log ALL THE user and bot conversation in database
        public static void ConversationLogg(string UserName, string UserRequest, string BotResponse, DateTime LoggingTime, string Channel)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("Conversationlogg", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 1000).Value = UserName;
                    sqlCommand.Parameters.Add("@UserRequest", SqlDbType.NVarChar, 4000).Value = UserRequest;
                    sqlCommand.Parameters.Add("@BotResponse", SqlDbType.NVarChar, 4000).Value = BotResponse;
                    sqlCommand.Parameters.Add("@LoggingTime", SqlDbType.DateTime).Value = LoggingTime;
                    sqlCommand.Parameters.Add("@Channel", SqlDbType.NVarChar, 100).Value = Channel;
                    sqlCommand.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                errorloginfo(ex.Message, DateTime.Now);
            }
        }

        // method to log the it Ticket which is generated
        public static void ItTicketLogging(string UserName, string IssueCategory, string IssueFacingWith,string IssueDescription,DateTime GeneratingTime, string Channel)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("ItTicketsGeneratedLog", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 1000).Value = UserName;
                    sqlCommand.Parameters.Add("@IssueCategory", SqlDbType.NVarChar, 4000).Value = IssueCategory;
                    sqlCommand.Parameters.Add("@IssueFacingWith", SqlDbType.NVarChar, 4000).Value = IssueFacingWith;
                    sqlCommand.Parameters.Add("@IssueDescription", SqlDbType.NVarChar, 4000).Value = IssueDescription;
                    sqlCommand.Parameters.Add("@GeneratingTime", SqlDbType.DateTime).Value = GeneratingTime;
                    sqlCommand.Parameters.Add("@Channel", SqlDbType.NVarChar, 100).Value = Channel;
                    sqlCommand.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                errorloginfo(ex.Message, DateTime.Now);
            }
        }


    }
}
