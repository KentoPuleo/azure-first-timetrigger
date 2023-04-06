using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Company.Function
{
    public class TimerTrigger2
    {
        [FunctionName("TimerTrigger2")]
        public static void Run([TimerTrigger("0 */20 * * * *")]TimerInfo myTimer, ILogger log)
        {
            var config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        var connectionString = config["mysql://root:admin@localhost:3306/mydb"];
        string sql = "SELECT date FROM list";

         using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                DateTime dateValue = (DateTime)reader["date"];
                log.LogInformation(dateValue.ToString());
            }
            reader.Close();
        }
        Console.WriteLine("Data retrieval process is complete.");
        }
    }
}
    