using MySqlConnector;
using Server_Side.Database_Services.Table_Interface;

namespace Server_Side.Database_Services.Input_Schema.Raw_Data_Tables_Class
{
    public class Pageview_table : Input_Tables_Template
    {
        internal class PageView
        {
            public string SessionId { get; set; }
            public string UserId { get; set; }
            public string PageUrl { get; set; }
            public string PageInfo { get; set; }
            public string ProductId { get; set; }
            public DateTime DateTime { get; set; }
            public DateTime Start_Time { get; set; }
            public DateTime End_Time { get; set; }
        }

        // Class Attributes
        private readonly string table_name = "pageview";
        private readonly string schemma = "analysis_and_reporting_raw_data";
        private string connect_String { get; set; } = "";
        private string Session_ID { get; set; } = "";

        private bool Connected_Status { get; set; } = false;
        private bool Created_Status { get; set; } = false;

        private static Pageview_table? pageview_table;
        private List<PageView> pageViews = new List<PageView>();
        private Pageview_table(string session_ID)
        {
            this.Created_Status = true;
            this.Connected_Status = false;
            this.Session_ID = session_ID;
            connect_String = "server=analysisreportingmoduledatabasegroup1.mysql.database.azure.com; uid=analysisreportingmodulegroup1;pwd=Conkhunglongtovai1;database=" + schemma + ";SslMode=Required";
        }

        public static Input_Tables_Template SetUp(string session_ID)
        {
            pageview_table = new Pageview_table(session_ID);
            return pageview_table;
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
                    var pageView = new PageView
                    {
                        SessionId = reader.GetString(0),
                        PageUrl = reader.GetString(1),
                        PageInfo = reader.GetString(2),
                        ProductId = reader.GetString(3),
                        Start_Time = reader.GetDateTime(4),
                        End_Time = reader.GetDateTime(5),
                        UserId = reader.GetString(6),
                    };
                    pageViews.Add(pageView);
                }

                //foreach (PageView pageView in pageViews)
                //{
                //    Console.WriteLine($"SessionId: {Session_ID}, PageUrl: {pageView.PageUrl}, PageInfo: {pageView.PageInfo}, ProductId: {pageView.ProductId}, Start_Time: {pageView.Start_Time}, End_Time: {pageView.End_Time}, UserId: {pageView.UserId}");
                //}
                await Connection.CloseAsync();
                return pageViews.Cast<object>().ToList();
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
            pageViews.Clear();
            pageViews = new List<PageView>();
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
