using MySqlConnector;
using Server_Side.Database_Services.Table_Interface;

namespace Server_Side.Database_Services.Output_Schema.Log_Database_Schema
{
    public class Analysis_and_reporting_log_data_table : Output_Tables_Template
    {
        private class Log_Item
        {
            public int Log_ID { get; set; }
            public DateTime Date_Access { get; set; }
            public string Requests { get; set; }
            public string Session_ID { get; set; }
        }
        // Class Attributes
        private readonly string table_name = "analysis_and_reporting_log_data";
        private readonly string schemma = "analysis_and_reporting_log_data";
        private string connect_String { get; set; } = "";
        private string Session_ID { get; set; } = "";

        private bool Connected_Status { get; set; } = false;
        private bool Created_Status { get; set; } = false;

        private static Analysis_and_reporting_log_data_table? analysis_and_reporting_log_data_table;
        private List<Log_Item> Log_Item_List = new List<Log_Item>();
        private Analysis_and_reporting_log_data_table(string session_ID)
        {
            this.Created_Status = true;
            this.Connected_Status = false;
            this.Session_ID = session_ID;
            connect_String = "server=analysisreportingmoduledatabasegroup1.mysql.database.azure.com; uid=analysisreportingmodulegroup1;pwd=Conkhunglongtovai1;database=" + schemma + ";SslMode=Required";
        }

        public static Output_Tables_Template SetUp(string session_ID)
        {
            analysis_and_reporting_log_data_table = new Analysis_and_reporting_log_data_table(session_ID);
            return analysis_and_reporting_log_data_table;
        }

        public async Task<bool> Create_Async(DateTime date, string request, string Session_ID)
        {
            try
            {
                using MySqlConnection Connection = new MySqlConnection(connect_String);
                await Connection.OpenAsync();
                Connected_Status = true;
                string formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                string sql = "INSERT INTO " + schemma + "." + table_name + "(Date_Access, Requests, Session_ID) VALUES('" + formattedDate + "', '" + request + "', '" + Session_ID + "');";
                using var cmd = new MySqlCommand(sql, Connection);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var log_item = new Log_Item
                    {
                        Log_ID = reader.GetInt32(0),
                        Date_Access = reader.GetDateTime(1),
                        Requests = reader.GetString(2),
                        Session_ID = reader.GetString(3),
                    };
                    Log_Item_List.Add(log_item);
                }
                await Connection.CloseAsync();
                return true;
            }
            catch (Exception ex)
            {
                Connected_Status = false;
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public async Task<List<object>?> Read_All_Async()
        {
            try
            {
                using MySqlConnection Connection = new MySqlConnection(connect_String);
                await Connection.OpenAsync();
                Connected_Status = true;

                string sql = $"SELECT * FROM {schemma}.{table_name};";
                using var cmd = new MySqlCommand(sql, Connection);
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var log_item = new Log_Item
                    {
                        Log_ID = reader.GetInt32(0),
                        Date_Access = reader.GetDateTime(1),
                        Requests = reader.GetString(2),
                        Session_ID = reader.GetString(3),
                    };
                    Log_Item_List.Add(log_item);
                }

                //foreach (Feedback feedback in feedback_list)
                //{
                //    Console.WriteLine($"SessionId: {Session_ID}, FeedbackId: {feedback.FeedbackId}, UserId: {feedback.UserId}, ProductId: {feedback.ProductId}, StarRating: {feedback.StarRating}");
                //}
                await Connection.CloseAsync();
                return Log_Item_List.Cast<object>().ToList();
            }
            catch (Exception ex)
            {
                Connected_Status = false;
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task Update_Async()
        {
            Log_Item_List.Clear();
            Log_Item_List = new List<Log_Item>();
            await Read_All_Async();
        }
        public bool Test_Connection_To_Table()
        {
            // Safety check to make sure the connection is working properly with simple open and close
            if (connect_String != null)
            {
                try
                {
                    MySqlConnection Connection = new MySqlConnection(connect_String);
                    Connection.Open();
                    Connected_Status = true;
                    Connection.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    Connected_Status = false;
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
