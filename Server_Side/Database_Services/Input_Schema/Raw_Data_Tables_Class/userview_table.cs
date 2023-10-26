using MySqlConnector;
using Server_Side.Database_Services.Table_Interface;

namespace Server_Side.Database_Services.Input_Schema.Raw_Data_Tables_Class
{
    public class Userview_table : Input_Tables_Template
    {
        internal class UserView
        {
            public string User_Id { get; set; }
            public DateTime Timestamp { get; set; }
            public DateOnly End_Date { get; set; }
            public DateOnly Start_Date { get; set; }
        }
        // Class Attributes
        private readonly string table_name = "userview";
        private readonly string schemma = "analysis_and_reporting_raw_data";
        private string connect_String { get; set; } = "";
        private string Session_ID { get; set; } = "";

        private bool Connected_Status { get; set; } = false;
        private bool Created_Status { get; set; } = false;

        private static Userview_table? userview_Table;
        private List<UserView> userViews = new List<UserView>();
        private Userview_table(string session_ID)
        {
            this.Created_Status = true;
            this.Connected_Status = false;
            this.Session_ID = session_ID;
            connect_String = "server=analysisreportingmoduledatabasegroup1.mysql.database.azure.com; uid=analysisreportingmodulegroup1;pwd=Conkhunglongtovai1;database=" + schemma + ";SslMode=Required";
        }

        public static Input_Tables_Template SetUp(string session_ID)
        {
            userview_Table = new Userview_table(session_ID);
            return userview_Table;
        }

        public async Task<List<object>?> ReadAllAsync()
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
                    var userView = new UserView
                    {
                        User_Id = reader.GetString("User_Id"),
                        Start_Date = reader.GetDateOnly("Start_Date"),
                        End_Date = reader.GetDateOnly("End_Date"),
                        Timestamp = reader.GetDateTime("Timestamp")
                    };
                    userViews.Add(userView);
                }

                //foreach (UserView userView in userViews)
                //{
                //    Console.WriteLine($"SessionId: {Session_ID}, User_Id: {userView.User_Id}, Start_Date: {userView.Start_Date}, End_Date: {userView.End_Date}, Timestamp: {userView.Timestamp}");
                //}
                await Connection.CloseAsync();
                return userViews.Cast<object>().ToList();
            }
            catch (Exception ex)
            {
                Connected_Status = false;
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task UpdateAsync()
        {
            userViews.Clear();
            userViews = new List<UserView>();
            await ReadAllAsync();
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

        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Insert()
        {
            throw new NotImplementedException();
        }
    }
}
