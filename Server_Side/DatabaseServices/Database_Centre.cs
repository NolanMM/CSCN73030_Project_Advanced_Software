using Server_Side.DatabaseServices.Services;
using Server_Side.DatabaseServices.Services.Interface_Service;
using Server_Side.DatabaseServices.Services.Models.Interfaces;

namespace Server_Side.DatabaseServices
{
    public abstract class Database_Centre
    {
        private static string? Id { get; set; }
        private static readonly Random random = new Random();
        private static readonly HashSet<short> usedSessionIDs = new HashSet<short>();
        private static IDatabaseServices? databaseServices { get; set; }
        public static async Task<List<Group_1_Record_Abstraction>?> GetDataForDatabaseServiceID(int tablenumber)
        {
            Id = GenerateUniqueSessionID();
            List<Group_1_Record_Abstraction>? processedData = new List<Group_1_Record_Abstraction>();
            switch (tablenumber)
            {
                case 0:
                    databaseServices = new UserViewTableService();
                    processedData = await databaseServices.GetDataServiceAsync();
                    break;
                case 1:
                    databaseServices = new PageViewTableService();
                    processedData = await databaseServices.GetDataServiceAsync();
                    break;
                case 2:
                    databaseServices = new SaleTransactionTableService();
                    processedData = await databaseServices.GetDataServiceAsync();
                    break;
                case 3:
                    databaseServices = new FeedbackTableService();
                    processedData = await databaseServices.GetDataServiceAsync();
                    break;
                default:
                    throw new ArgumentException("Invalid table number");
            }
            return processedData;
        }
        private static string GenerateUniqueSessionID()
        {
            short sessionId;
            do
            {
                sessionId = (short)random.Next(0, 65536);
            } while (usedSessionIDs.Contains(sessionId));
            usedSessionIDs.Add(sessionId);
            return sessionId.ToString();
        }
    }
}
