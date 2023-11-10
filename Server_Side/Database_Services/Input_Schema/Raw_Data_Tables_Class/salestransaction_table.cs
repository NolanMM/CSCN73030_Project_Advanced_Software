using MySqlConnector;
using Server_Side.Database_Services.Table_Interface;
using static Server_Side.Services.Analysis_Report_Services;

namespace Server_Side.Database_Services.Input_Schema.Raw_Data_Tables_Class
{
    public class Salestransaction_table : Input_Tables_Template
    {
        //internal class SaleTransaction
        //{
        //    public string TransactionId { get; set; }
        //    public string UserId { get; set; }
        //    public decimal TransactionValue { get; set; }
        //    public DateTime Date { get; set; }
        //}

        // Class Attributes
        private readonly string table_name = "sales_transaction_table";
        private readonly string schemma = "analysis_and_reporting_raw_data";
        private string connect_String { get; set; } = "";
        private string Session_ID { get; set; } = "";

        private bool Connected_Status { get; set; } = false;
        private bool Created_Status { get; set; } = false;

        private static Salestransaction_table? salestransaction_table;
        private List<SaleTransaction> saleTransaction_list = new List<SaleTransaction>();
        private Salestransaction_table(string session_ID)
        {
            this.Created_Status = true;
            this.Connected_Status = false;
            this.Session_ID = session_ID;
            connect_String = "server=analysisreportingmoduledatabasegroup1.mysql.database.azure.com; uid=analysisreportingmodulegroup1;pwd=Conkhunglongtovai1;database=" + schemma + ";SslMode=Required";
        }

        public static Input_Tables_Template SetUp(string session_ID)
        {
            salestransaction_table = new Salestransaction_table(session_ID);
            return salestransaction_table;
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
                    var saleTransaction = new SaleTransaction
                    {
                        TransactionId = reader.GetString(0),
                        UserId = reader.GetString(1),
                        TransactionValue = reader.GetDecimal(2),
                        Date = reader.GetDateTime(3),
                    };
                    saleTransaction_list.Add(saleTransaction);
                }

                //foreach (SaleTransaction saleTransaction in saleTransaction_list)
                //{
                //    Console.WriteLine($"SessionId: {Session_ID}, TransactionId: {saleTransaction.TransactionId}, UserId: {saleTransaction.UserId}, TransactionValue: {saleTransaction.TransactionValue}, Date: {saleTransaction.Date}");
                //}
                await Connection.CloseAsync();
                return saleTransaction_list.Cast<object>().ToList();
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
            saleTransaction_list.Clear();
            saleTransaction_list = new List<SaleTransaction>();
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
