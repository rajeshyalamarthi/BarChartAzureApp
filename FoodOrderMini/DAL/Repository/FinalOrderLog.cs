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
   public class FinalOrderLog
    {
     public static String connectionstring = ConfigurationManager.ConnectionStrings["FinalOrderLog"].ConnectionString;


        public static void FinalOrderInfo(string UserName,string FinalOrder, DateTime OrderTime,string Channel)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionstring))
                {

                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("FinalOrderProc", connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add("@action", SqlDbType.NVarChar, 10).Value = "INSERT";
                    sqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 1000).Value = UserName;
                    sqlCommand.Parameters.Add("@FinalOrder", SqlDbType.NVarChar, 4000).Value = FinalOrder;
                    sqlCommand.Parameters.Add("@OrderTime", SqlDbType.DateTime).Value = OrderTime;
                    sqlCommand.Parameters.Add("@Channel", SqlDbType.NVarChar,100).Value = Channel;
                   
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
