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
    public static class PostResponse
    {
        public static string BotResponseFromDB;
        public static List<TicketsList> ticketsLists = new List<TicketsList>();
        public static List<QuestionsList> questionsLists = new List<QuestionsList>();
        public static String connectionstring = ConfigurationManager.ConnectionStrings["LccChatBot"].ConnectionString;
        // method to get the response from db for static questions
        public static  string GetResponseFromBotForStaticQuestions(string Intent)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("StaticQuestionsProc", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;                   
                    sqlCommand.Parameters.Add("@pIntent", SqlDbType.NVarChar, 100).Value = Intent;
                    sqlCommand.Parameters.Add("@pBotResponse", SqlDbType.NVarChar, 4000);
                    sqlCommand.Parameters["@pBotResponse"].Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();

                    BotResponseFromDB = (string) sqlCommand.Parameters["@pBotResponse"].Value;
                

                    connection.Close();

                    return BotResponseFromDB;

                }
            }
            catch (Exception ex)
            {
                Logging.errorloginfo(ex.Message, DateTime.Now);
                return null;
            }
        }

        // method to get the response from db for Dynamic questions
        public static string GetResponseFromBotForDynamicQuestions(string Intent,string Entity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("DynamicQuestionsProc", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pIntent", SqlDbType.NVarChar, 100).Value = Intent;
                    sqlCommand.Parameters.Add("@pEntity", SqlDbType.NVarChar, 100).Value = Entity;
                    sqlCommand.Parameters.Add("@pBotResponse", SqlDbType.NVarChar, 4000);
                    sqlCommand.Parameters["@pBotResponse"].Direction = ParameterDirection.Output;
                    sqlCommand.ExecuteNonQuery();

                    BotResponseFromDB = (string)sqlCommand.Parameters["@pBotResponse"].Value;
                    connection.Close();

                    return BotResponseFromDB;

                }
            }
            catch (Exception ex)
            {
                Logging.errorloginfo(ex.Message, DateTime.Now);
                return ex.Message;
            }
        }

        public static List<TicketsList> TicketsLists(string UserName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("TicketsGeneratedCount", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pUsername", SqlDbType.NVarChar, 100).Value = UserName;
                    sqlCommand.ExecuteNonQuery();
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            ticketsLists.Add(new TicketsList
                            {
                                IssueCategory = (sqlDataReader["IssueCategory"].ToString()),
                                IssuefacingWith = (sqlDataReader["IssuefacingWith"].ToString()),
                                IssueDescription = (sqlDataReader["IssueDescription"].ToString()),
                                GeneratingTime = (sqlDataReader["GeneratingTime"].ToString())
                            });

                        }
                    }

                }

                return ticketsLists;
            }
            catch (Exception ex)
            {
                Logging.errorloginfo(ex.Message, DateTime.Now);
                return null;
            }
        }





        public static List<QuestionsList> Questions(string Intent)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("QuestionList", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@pIntent", SqlDbType.NVarChar, 100).Value = Intent;
                    sqlCommand.ExecuteNonQuery();
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            questionsLists.Add(new QuestionsList
                            {
                                Questions = (sqlDataReader["Questions"].ToString())
                            });

                        }
                    }

                }

                return questionsLists;
            }
            catch (Exception ex)
            {
                Logging.errorloginfo(ex.Message, DateTime.Now);
                return null;
            }
        }


    }
}
